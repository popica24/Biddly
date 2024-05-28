# Biddly

## Note: This project is developed as the final project to complete a Bachelor's degree in Computer Science.

This project is a robust and scalable bidding platform designed to showcase various modern web development techniques and technologies. It features a .NET Web API backend, a React and TypeScript frontend, Redis caching, and a PostgreSQL database. The architecture follows best practices such as Onion Architecture, Repository Pattern, and includes manual JWT authentication and authorization.

## Key Features

- **Manual JWT Authentication and Authorization**: Secure user authentication implemented from scratch to demonstrate knowledge.
- **SignalR for Real-Time Bids**: Real-time updates for bids using SignalR for a seamless bidding experience.
- **Background Service for Bid Expiration**: A background service continuously checks and handles bid expirations.
- **Onion Architecture**: Ensures a clean separation of concerns and maintainable code structure.
- **REST API**: Follows REST principles for building scalable and easy-to-use API endpoints.
- **Dapper as ORM**: Lightweight ORM for efficient database operations.
- **Redis**: Used for caching bids that are upcoming or in process and storing the winning items in a persistent database.
- **Frontend Stack**: Built with React, TypeScript, Tailwind CSS, and Flowbite for a responsive and modern user interface.
- **Repository Pattern**: Utilized in the frontend to access the REST API cleanly and efficiently.

## Technology Stack

**Backend**

- .NET Web API
- SignalR
- Dapper
- Redis
- PostgreSQL

**Frontend**

- React
- TypeScript
- Tailwind CSS
- Flowbite

## License

This project is licensed under the MIT License. See the LICENSE file for more details.

## Contact

For any queries or support, please contact andreip927@gmail.com

##

This project aims to provide a high-performance, user-friendly bidding platform suitable for various auction scenarios. Enjoy bidding!
