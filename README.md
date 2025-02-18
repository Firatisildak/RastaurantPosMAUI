<p align="center">
  <img src="https://github.com/user-attachments/assets/a7c0b841-7d7c-4f32-b998-8ada82def205" width="500">
  <img src="https://github.com/user-attachments/assets/693109dc-caee-4692-b424-64a409d10c5c" width="500">
  <img src="https://github.com/user-attachments/assets/76633ad5-5661-4f3f-83f3-892e56e628e7" width="500">
  <img src="https://github.com/user-attachments/assets/7fa98c19-340a-4135-a27c-243abd894107" width="500">
</p>


# Restaurant Management App

## Project Description

A cross-platform restaurant management application built with **.NET MAUI**, **Entity Framework Core**, and **MVVM architecture**. This app allows users to browse meals, add them to their cart, place orders with online or cash payment options, and review past orders. Additionally, it provides CRUD operations for managing meals.

## Features

- **Meal Selection**: Browse available meals and select items to add to the cart.
- **Cart Management**: Add, remove, or update meal quantities in the cart.
- **Order Placement**: Place orders with the choice of online or cash payment methods.
- **Order History**: View previous orders and their details.
- **Admin Features**:
  - Add new meals to the menu.
  - Edit or delete existing meals.
- **Cross-Platform**: Supports Android, iOS, Windows, and macOS.
- **Modern Architecture**: Built using the MVVM design pattern for scalability and maintainability.

## Technology Stack

- **Frontend**: .NET MAUI
- **Backend**: Entity Framework Core
- **Database**: SQLite
- **Architecture**: MVVM Pattern

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/restaurant-management-app.git
   ```

2. Navigate to the project directory:
   ```bash
   cd restaurant-management-app
   ```

3. Restore NuGet packages:
   ```bash
   dotnet restore
   ```

4. Build and run the project:
   ```bash
   dotnet build
   dotnet run
   ```

## Usage

### User Features

1. **Browse Meals**: Navigate through the available meals.
2. **Add to Cart**: Select meals and add them to the cart.
3. **Place Orders**: Choose a payment method (online or cash) and place the order.
4. **View Order History**: Check details of past orders.

### Admin Features

1. **Add Meals**: Add new meals to the menu with details like name, price, and description.
2. **Edit Meals**: Update existing meal information.
3. **Delete Meals**: Remove meals from the menu.

## Screenshots

_Add screenshots of the app's key features, such as meal browsing, cart management, and order history._

## Project Structure

```markdown
RestaurantManagementApp
├── Models
│   ├── CartModel.cs
│   ├── MenuCategoryModel.cs
│   ├── MenuItemChangedMessage.cs
│   ├── MenuItemModel.cs
│   ├── NameChangedMessage.cs
│   └── OrderModel.cs
├── ViewModels
│   ├── HomeViewModel.cs
│   ├── ManageMenuItemsViewModel.cs
│   ├── OrdersViewModel.cs
│   └── SettingsViewModel.cs
├── Views(Pages)
│   ├── MainPage.xaml
│   ├── ManageMenuItemPage.xaml
│   └── OrdersPage.xaml
├── Data
│   ├── Entities.cs
│   ├── AppDbContext.cs
│   └── SeedData.cs
├── Services
│   ├── DatabaseService.cs
├── App.xaml
├── App.xaml.cs
└── Program.cs
```

## Contributing

Contributions are welcome! To contribute:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature/YourFeature`).
3. Commit your changes (`git commit -m 'Add a new feature'`).
4. Push to the branch (`git push origin feature/YourFeature`).
5. Open a pull request.


## Contact

For any inquiries, please reach out:
- Email: firatisildak1@gmail.com

Thank you for checking out the Restaurant Management App! 😊
