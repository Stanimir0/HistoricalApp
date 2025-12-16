using HistoricalApp.Models;
using HistoricalApp.Services;
using Microsoft.Maui.Storage;
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

            _editingUser.UserName = UserName;
            _editingUser.Description = Description;
            _editingUser.ProfileImage = _base64Image;

            await _userService.UpdateUserAsync(_editingUser.Id, _editingUser);
            
            // Notify user and go back
            await Shell.Current.DisplayAlert("Success", "Profile updated successfully!", "OK");
            await Shell.Current.Navigation.PopAsync();
        }
    }
}
