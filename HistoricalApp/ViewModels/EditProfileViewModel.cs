using HistoricalApp.Helpers;
using HistoricalApp.Models;
using HistoricalApp.Services;
using Microsoft.Maui.Storage;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HistoricalApp.ViewModels
{
    public class EditProfileViewModel : BaseViewModel
    {
        private readonly FirebaseUserService _userService;
        private User _editingUser;

        public ICommand PickImageCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand SelectBadgeCommand { get; }
        public ICommand RemoveBadgeCommand { get; }
        public ICommand SelectBorderCommand { get; }
        public ICommand RemoveBorderCommand { get; }

        public TranslationService Translations => TranslationService.Instance;

        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }
        private string _userName;

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        private string _description;

        public ImageSource ProfileImageSource
        {
            get => _profileImageSource;
            set => SetProperty(ref _profileImageSource, value);
        }
        private ImageSource _profileImageSource;

        public ObservableCollection<ShopItem> PurchasedBadges { get; set; } = new();
        public ObservableCollection<ShopItem> PurchasedBorders { get; set; } = new();

        public ShopItem SelectedBadge
        {
            get => _selectedBadge;
            set => SetProperty(ref _selectedBadge, value);
        }
        private ShopItem _selectedBadge;

        public ShopItem SelectedBorder
        {
            get => _selectedBorder;
            set => SetProperty(ref _selectedBorder, value);
        }
        private ShopItem _selectedBorder;

        public bool HasSelectedBadge => SelectedBadge != null;
        public bool HasSelectedBorder => SelectedBorder != null;

        private string _base64Image;

        public EditProfileViewModel(User user)
        {
            _userService = new FirebaseUserService();
            _editingUser = user;

            // Initialize fields
            UserName = user.UserName;
            Description = user.Description;
            _base64Image = user.ProfileImage;

            LoadImage(user.ProfileImage);

            PickImageCommand = new Command(async () => await PickImage());
            SaveCommand = new Command(async () => await SaveChanges());
            CancelCommand = new Command(async () => await Shell.Current.Navigation.PopAsync());
            SelectBadgeCommand = new Command<ShopItem>(SelectBadge);
            RemoveBadgeCommand = new Command(RemoveBadge);
            SelectBorderCommand = new Command<ShopItem>(SelectBorder);
            RemoveBorderCommand = new Command(RemoveBorder);

            // Load purchased items
            _ = LoadPurchasedItems();
        }

        private async Task LoadPurchasedItems()
        {
            try
            {
                var userId = _editingUser.Id;
                var purchasedItemIds = await _userService.GetPurchasedItemsAsync(userId);
                var allShopItems = ShopService.GetShopItems();

                // Filter badges
                var badges = allShopItems
                    .Where(item => item.Category == "Badge" && purchasedItemIds.Contains(item.Id))
                    .ToList();

                // Filter borders
                var borders = allShopItems
                    .Where(item => item.Category == "Border" && purchasedItemIds.Contains(item.Id))
                    .ToList();

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    PurchasedBadges.Clear();
                    foreach (var badge in badges)
                    {
                        PurchasedBadges.Add(badge);
                    }

                    PurchasedBorders.Clear();
                    foreach (var border in borders)
                    {
                        PurchasedBorders.Add(border);
                    }

                    // Set currently equipped items
                    if (!string.IsNullOrEmpty(_editingUser.EquippedBadge))
                    {
                        SelectedBadge = PurchasedBadges.FirstOrDefault(b => b.Id == _editingUser.EquippedBadge);
                    }

                    if (!string.IsNullOrEmpty(_editingUser.EquippedBorder))
                    {
                        SelectedBorder = PurchasedBorders.FirstOrDefault(b => b.Id == _editingUser.EquippedBorder);
                    }
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading purchased items: {ex.Message}");
            }
        }

        private void SelectBadge(ShopItem badge)
        {
            SelectedBadge = badge;
            OnPropertyChanged(nameof(HasSelectedBadge));
        }

        private void RemoveBadge()
        {
            SelectedBadge = null;
            OnPropertyChanged(nameof(HasSelectedBadge));
        }

        private void SelectBorder(ShopItem border)
        {
            SelectedBorder = border;
            OnPropertyChanged(nameof(HasSelectedBorder));
        }

        private void RemoveBorder()
        {
            SelectedBorder = null;
            OnPropertyChanged(nameof(HasSelectedBorder));
        }

        private void LoadImage(string base64)
        {
            if (!string.IsNullOrEmpty(base64))
            {
                try
                {
                    var imageBytes = Convert.FromBase64String(base64);
                    ProfileImageSource = ImageSource.FromStream(() => new MemoryStream(imageBytes));
                }
                catch
                {
                    ProfileImageSource = "dotnet_bot.png";
                }
            }
            else
            {
                ProfileImageSource = "dotnet_bot.png";
            }
        }

        private async Task PickImage()
        {
            try
            {
                var result = await FilePicker.Default.PickAsync(new PickOptions
                {
                    PickerTitle = "Select Profile Picture",
                    FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                    {
                        { DevicePlatform.iOS, new[] { "public.image" } },
                        { DevicePlatform.Android, new[] { "image/*" } },
                        { DevicePlatform.WinUI, new[] { ".jpg", ".jpeg", ".png" } }
                    })
                });

                if (result != null)
                {
                    using var stream = await result.OpenReadAsync();
                    using var memoryStream = new MemoryStream();
                    await stream.CopyToAsync(memoryStream);
                    var imageBytes = memoryStream.ToArray();

                    _base64Image = Convert.ToBase64String(imageBytes);
                    ProfileImageSource = ImageSource.FromStream(() => new MemoryStream(imageBytes));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error picking file: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Failed to pick image.", "OK");
            }
        }

        private async Task SaveChanges()
        {
            if (string.IsNullOrWhiteSpace(UserName))
            {
                await Shell.Current.DisplayAlert("Error", "Username cannot be empty.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(_editingUser.Id))
            {
                await Shell.Current.DisplayAlert("Error", "Invalid user data. Please try again.", "OK");
                return;
            }

            _editingUser.UserName = UserName;
            _editingUser.Description = Description;
            _editingUser.ProfileImage = _base64Image;
            
            // Update equipped items
            _editingUser.EquippedBadge = SelectedBadge?.Id ?? string.Empty;
            _editingUser.EquippedBorder = SelectedBorder?.Id ?? string.Empty;

            await _userService.UpdateUserAsync(_editingUser.Id, _editingUser);
            
            // Notify user and go back
            await Shell.Current.DisplayAlert("Success", "Profile updated successfully!", "OK");
            await Shell.Current.Navigation.PopAsync();
        }
    }
}
