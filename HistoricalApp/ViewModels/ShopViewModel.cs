using HistoricalApp.Helpers;
using HistoricalApp.Models;
using HistoricalApp.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HistoricalApp.ViewModels
{
    public class ShopViewModel : BaseViewModel
    {
        private readonly FirebaseUserService _userService;
        private readonly FirebaseAuthService _authService;
        private int _userCurrency;
        private string _selectedCategory = "All";

        public ObservableCollection<ShopItem> ShopItems { get; set; } = new();
        public ObservableCollection<ShopItem> FilteredItems { get; set; } = new();
        public ObservableCollection<string> Categories { get; set; } = new();

        public int UserCurrency
        {
            get => _userCurrency;
            set
            {
                _userCurrency = value;
                OnPropertyChanged();
            }
        }

        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
                FilterItems();
            }
        }

        public TranslationService Translations => TranslationService.Instance;

        public ICommand LoadShopCommand { get; }
        public ICommand PurchaseItemCommand { get; }
        public ICommand RefreshCommand { get; }

        public ShopViewModel()
        {
            _userService = new FirebaseUserService();
            _authService = new FirebaseAuthService();

            LoadShopCommand = new Command(async () => await LoadShop());
            PurchaseItemCommand = new Command<ShopItem>(async (item) => await PurchaseItem(item));
            RefreshCommand = new Command(async () => await LoadShop());
        }

        private async Task LoadShop()
        {
            try
            {
                // Load shop items
                var items = ShopService.GetShopItems();
                
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    ShopItems.Clear();
                    foreach (var item in items)
                    {
                        ShopItems.Add(item);
                    }
                });

                // Load categories
                var categories = ShopService.GetCategories();
                categories.Insert(0, "All");
                
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Categories.Clear();
                    foreach (var category in categories)
                    {
                        Categories.Add(category);
                    }
                });

                // Load user currency
                var userId = Preferences.Get("UserId", string.Empty);
                if (!string.IsNullOrEmpty(userId))
                {
                    var user = await _userService.GetUserByIdAsync(userId);
                    if (user != null)
                    {
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            UserCurrency = user.Currency;
                        });

                        // Mark purchased items
                        var purchasedItems = await _userService.GetPurchasedItemsAsync(userId);
                        if (purchasedItems != null && purchasedItems.Count > 0)
                        {
                            MainThread.BeginInvokeOnMainThread(() =>
                            {
                                foreach (var item in ShopItems)
                                {
                                    if (purchasedItems.Contains(item.Id))
                                    {
                                        item.IsPurchased = true;
                                    }
                                }
                            });
                        }
                    }
                }

                FilterItems();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading shop: {ex.Message}");
            }
        }

        private void FilterItems()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                FilteredItems.Clear();
                var itemsToShow = SelectedCategory == "All" 
                    ? ShopItems 
                    : ShopItems.Where(i => i.Category == SelectedCategory);

                foreach (var item in itemsToShow)
                {
                    FilteredItems.Add(item);
                }
            });
        }

        private async Task PurchaseItem(ShopItem item)
        {
            if (item == null) return;

            try
            {
                var userId = Preferences.Get("UserId", string.Empty);
                if (string.IsNullOrEmpty(userId))
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        "You must be logged in to purchase items.",
                        "OK"
                    );
                    return;
                }

                // Check if user has enough currency
                if (UserCurrency < item.Price)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        Translations.InsufficientFunds,
                        $"You need {item.Price - UserCurrency} more coins.",
                        "OK"
                    );
                    return;
                }

                // Handle consumable powerups differently
                bool isPowerup = item.Category.Equals("Powerup", StringComparison.OrdinalIgnoreCase);

                // For non-powerups, check if already purchased
                if (!isPowerup && item.IsPurchased)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Already Owned",
                        "You already own this item.",
                        "OK"
                    );
                    return;
                }

                // Confirm purchase
                string confirmMsg = isPowerup
                    ? $"Purchase {item.Name} for {item.Price} coins? (consumable)"
                    : $"Purchase {item.Name} for {item.Price} coins?";

                bool confirm = await Application.Current.MainPage.DisplayAlert(
                    Translations.Purchase,
                    confirmMsg,
                    "Yes",
                    "No"
                );

                if (!confirm) return;

                if (isPowerup)
                {
                    // Consumable purchase — deduct currency, add to inventory
                    var user = await _userService.GetUserByIdAsync(userId);
                    if (user == null) return;

                    user.Currency -= item.Price;

                    if (item.Id == "hint_fifty_fifty")
                        user.HintFiftyFifty++;
                    else if (item.Id == "hint_double_points")
                        user.HintDoublePoints++;

                    await _userService.UpdateUserAsync(userId, user);
                    UserCurrency = user.Currency;

                    await Application.Current.MainPage.DisplayAlert(
                        "Success",
                        $"You purchased {item.Name}! Use it during a quiz.",
                        "OK"
                    );
                }
                else
                {
                    // Regular cosmetic purchase
                    bool success = await _userService.PurchaseItemAsync(userId, item);

                    if (success)
                    {
                        UserCurrency -= item.Price;
                        item.IsPurchased = true;

                        await Application.Current.MainPage.DisplayAlert(
                            "Success",
                            $"You purchased {item.Name}! You can equip it in Edit Profile.",
                            "OK"
                        );
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert(
                            "Error",
                            "Purchase failed. Please try again.",
                            "OK"
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error purchasing item: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "An error occurred during purchase.",
                    "OK"
                );
            }
        }
    }
}
