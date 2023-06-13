## ACID
Atomicity: Do all or do nothing

Consistency: Consistency between data

Isolation: Transactions should be isolated from each other

Durability: Data should be stored safely

## RabbitMQ
- Publish:
    
    - Sends to Exchange
    - The message is lost is no one is listening

- Send:

    - Sends to a specific queue

## Saga Pattern

### Choreography-based saga
![Alt text](images/chroegraphy_based.PNG)

- If you have 2 - 4 microservices
- No central transaction manager. Therefore no performance bottleneck
- Uses a message broker system for async communication 
- Every service listens to message queue to take the necessary action and also sends action result to the queue 
- Local transaction order is used for transaction management
- Uses compensable (rollback transaction) transactions
  
![Alt text](images/chroegraphy_based_detail.PNG)

### Orchestration-based saga

![Alt text](images/orchestration_based.PNG)

- Uses a central transaction manager which may cause performance bottleneck
- If you have more than 4 microservices
- Uses async messaging pattern
- All transactions are managed by a central manager (Saga State Machine)

## Event Sourcing

- Data is saved as an append-only event to the db
- An entity's state can be acquired by playing the events
- Good for keeping history
- Entity's last state can be derived even after db crash
- Use with CQRS

### Event Store
- A db which is created for event sourcing
- Events are store in chronological order
- Has a notification system to listen saved events
- Open source
- Can be communicated via http or tcp


## Resiliency
The ability of software to heal from unexpected events 

### Retry Pattern
### Circuit Breaker Pattern