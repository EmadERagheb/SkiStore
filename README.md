

# Ski Net

Ski Net is an online store for purchasing ski equipment. This project demonstrates a full-fledged e-commerce application with various features including product browsing, filtering, basket management, checkout, and payment processing.

## Features

- **Product Listing and Filtering:** Browse through a list of ski equipment, filter by specific brands or types, and use the search functionality.
- **Pagination:** Navigate through multiple pages of products.
- **Basket Management:** Add products to the basket, view basket details, and see automatic order subtotal calculations.
- **User Authentication:** Log in to proceed with checkout using JWT access tokens and Microsoft Identity refresh tokens.
- **Checkout Process:** Automatically populate the shipping address for returning users, choose delivery options, and review the order summary.
- **Payment Processing:** Use Stripe as the payment processor, with webhooks for order confirmation.
- **Backend Services:**
  - **Entity Framework Code First:** For MS SQL Server provider.
  - **Redis DB:** For basket and cached data.
  - **State Service:** For client-side data caching.
  - **Loading Indicators and Global Notification System:** For improved user experience.
  - **General Exception Handler:** For handling errors seamlessly.

## Installation

### Prerequisites

- .NET SDK
- Node.js
- Angular CLI
- MS SQL Server
- Redis
- Stripe Account

### Backend Setup

1. Clone the repository:
   ```sh
   git clone https://github.com/yourusername/skinet.git
   cd skinet
   ```

2. Set up the database:
   ```sh
   dotnet ef database update
   ```

3. Configure the app settings:
   - Update the `appsettings.json` file with your MS SQL Server and Redis configurations.
   - Add your Stripe API keys.

4. Run the backend server:
   ```sh
   dotnet run
   ```

### Frontend Setup

1. Navigate to the `ClientApp` directory:
   ```sh
   cd ClientApp
   ```

2. Install dependencies:
   ```sh
   npm install
   ```

3. Run the frontend server:
   ```sh
   ng serve
   ```

## Usage

1. Open your browser and navigate to `http://localhost:4200`.
2. Browse products, filter, and search for specific items.
3. Add products to your basket and proceed to checkout.
4. Log in or register to complete the order.
5. Choose delivery options and review your order.
6. Use the Stripe test card to simulate payment.
7. Confirm the order and check the status in the Stripe dashboard.

## Contributing

We welcome contributions to Ski Net! Please follow these steps to contribute:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature/YourFeature`).
3. Commit your changes (`git commit -m 'Add some feature'`).
4. Push to the branch (`git push origin feature/YourFeature`).
5. Open a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Contact

For more information or support, please contact Emad at [your.email@example.com](mailto:your.email@example.com).

---

Feel free to modify this template according to your specific requirements.
