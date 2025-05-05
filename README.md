# Restaurants Web API

## Overview

A RESTful Web API built with **C#** and **.NET**, designed to manage restaurant data, including menus, orders, and user interactions. The application follows a layered architecture to ensure separation of concerns and maintainability.

## Technologies Used

- **Language:** C#
- **Framework:** .NET
- **Architecture:** Clean Architecture (Layered)
- **Other:** Entity Framework Core, ASP.NET Core Web API

## Project Structure

The solution is organized into several projects, each responsible for a specific layer or concern:
- **Restaurants.API** Web API layer (Controllers, Routes)
- **Restaurants.Application** Application logic (Use cases, Services)
- **Restaurants.Domain** Core domain models, entities and interfaces
- **Restaurants.Infrastructure** Data access, policy, seeders

## Features

#### Restaurant Management:
  - CRUD operations for restaurants
  - Manage restaurant details and menus
  - Categorize restaurants (e.g., vege, fast food)

#### Dishes Management:
  - CRUD operations for dishes

#### User Authentication:
  - Role-based access control (Admin, User)
