# PubSubDemo

Publish Subscribe demo using brighter library with two services - Audit module service and audit trail service. When audit data is created, 
the subscriber (audit trail service) will display the event created.

# How to run locally
1. Run Kafka UI 
    * Open command prompt and navigate to the root folder
    * Run the docker compose command
    ```sh
    docker-compose -f docker-compose-kafka.yaml up
    ```
2. Run AuditService
    * Open another command prompt and navigate to AuditService.API folder
    * Run the following command to start the audit module service
    ```sh
    dotnet run
    ```
3. Run AuditTrailService
    * Open another command prompt and navigate to AuditTrailService folder
    * Run
    ```sh
    dotnet run
    ```
