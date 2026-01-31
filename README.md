# FoodDelivery – DDD Modular Monolith

## Overview

This repository contains an **enterprise-grade modular monolith** for a Food Delivery system (Uber Eats–like), implemented using **Domain-Driven Design (DDD)** principles.

The system is intentionally designed as a **single deployable application** while maintaining **strong logical boundaries** between bounded contexts. The architecture supports scalability, long-term maintainability, and a smooth evolution path toward microservices if required.

---

## Architectural Goals

* Enforce **bounded context isolation** without microservice overhead
* Keep **domain models rich and expressive**
* Prevent accidental coupling between modules
* Support **event-driven collaboration** between contexts
* Remain **framework-agnostic at the domain level**

---

## High-Level Architecture

The solution follows a layered, modular structure:

* **Domain Layer** – Pure business logic and invariants
* **Application Layer** – Use cases and orchestration
* **Infrastructure Layer** – Persistence, messaging, logging
* **API Layer** – HTTP endpoints per module
* **Bootstrapper** – Composition root and module wiring

Each bounded context is implemented as an independent module within the monolith.

---

## Solution Structure

```
FoodDelivery.sln
│
├── src
│   ├── BuildingBlocks
│   │   ├── FoodDelivery.BuildingBlocks.Domain
│   │   ├── FoodDelivery.BuildingBlocks.Application
│   │   ├── FoodDelivery.BuildingBlocks.Infrastructure
│   │   └── FoodDelivery.BuildingBlocks.Integration
│   │
│   ├── Modules
│   │   ├── Catalog
│   │   ├── Delivery
│   │   ├── Identity
│   │   ├── Notification
│   │   ├── Ordering
│   │   ├── Payment
│   │   ├── Pricing (Promotions)
│   │   ├── Restaurants
│   │   ├── Reviews
│   │   └── Search
│   │   ├── Tracking (Location)
│   │   ├── UserProfile
│   │   ├── Payout (i'll introduce it later on)
│   │
│   ├── API
│   │   └── FoodDelivery.API
│   │
│   └── Bootstrapper
│       └── FoodDelivery.Bootstrapper
│
└── tests
```

---

## Bounded Contexts

Each module represents a **bounded context** with its own model and rules:

* **Ordering** – Order lifecycle and state transitions
* **Payments** – Payment processing and confirmation
* **Restaurants** – Restaurant availability and onboarding
* **Delivery** – Courier assignment and tracking
* **Notifications** – Customer and restaurant notifications

Modules do **not** reference each other’s domain models.

---

## Domain Layer

The Domain layer contains:

* Aggregates and entities
* Value objects
* Domain services (stateless business logic)
* Explicit business rules
* Domain events

### Example Structure

```
Ordering.Domain
├── Aggregates
├── ValueObjects
├── Rules
├── Services
└── Events
```

Key principles:

* No framework dependencies
* No persistence concerns
* All invariants enforced inside aggregates

---

## Domain Events

Domain events represent **important facts within a bounded context**.

Characteristics:

* Raised by aggregates
* Handled within the same bounded context
* Used to trigger side effects or publish integration events

Domain events **never cross bounded context boundaries**.

---

## Integration Events

Integration events are **stable contracts** used for communication between bounded contexts.

They:

* Represent facts that other contexts may react to
* Are published via the event bus
* Are consumed by other modules through integration event handlers

Integration events live in:

```
FoodDelivery.BuildingBlocks.Integration
```

No domain logic exists in integration events.

---

## Application Layer

The Application layer coordinates use cases and workflows.

It contains:

* Commands and command handlers
* Queries and query handlers
* Domain event handlers (outbound)
* Integration event handlers (inbound)

### Event Handling Responsibilities

* **Domain Event Handlers**

  * React to local domain events
  * Publish integration events

* **Integration Event Handlers**

  * React to events from other bounded contexts
  * Execute local use cases

---

## Event Bus

The system uses an **in-memory event bus** for integration events.

Key characteristics:

* In-process and synchronous
* No explicit subscribe API
* Subscription is achieved through DI registration

This design:

* Avoids global registries
* Preserves modularity
* Is easily replaceable with a real message broker

---

## Infrastructure Layer

The Infrastructure layer provides:

* EF Core persistence
* Unit of Work implementation
* Domain event dispatching
* Integration event publishing
* Logging

Infrastructure code depends on Application and Domain layers, never the reverse.

---

## Database Strategy

* Single physical database
* One schema per bounded context

Example:

```
ordering.*
payments.*
restaurants.*
```

This preserves isolation while keeping operational complexity low.

---

## Testing Strategy

The solution follows a multi-level testing approach:

* **Domain tests** – Pure unit tests for business rules
* **Application tests** – Command and handler tests
* **Integration tests** – EF Core and database behavior
* **End-to-end tests** – API-level validation

---

## Design Principles Enforced

* No shared domain models
* No cross-module repository access
* Explicit invariants via domain rules
* Event-driven collaboration
* Clear dependency direction

---

## Evolution Path

This architecture supports:

* Gradual extraction into microservices (and to be deployed as a kubernetes cluster later on)
* Replacement of the in-memory bus with a message broker
* Introduction of the Outbox pattern
* Independent scaling of bounded contexts

---

## Status

This project is intended to simulate the high quality softwares for enterprise systems using DDD and modular monolith architecture.

I prioritizes correctness, clarity, and long-term sustainability over short-term convenience.




Note: the project is not completed and still under development. i'm actively working on it :)
