An enterprise-grade designed online food delivery platform following canonical Domain Driven Design.
The project uses Multi bounded contexts, translated into modules that communicate asynchronously via Integration Events through In-Memory Event Bus for decoupling the producers from the consumers 
while still have one deployable unit (scalability and boundaries enforcment is going to be done perfectly without the costs of adopting microservices). 
i will migrate to microsevices, kafka, k8s and the other distributed systems' stack components later on but now the focus on the app itself and making it modular and friendly to be migrated smoothly.
The architecture leverages Redis for caching, ElasticSearch for search, Prometheus for Metrics scraping, and CI automation using Jenkins and with a strong emphasis on scalability, resilience, Idempotency and observability.

Note: the project is still under development and i'm actively working on it
