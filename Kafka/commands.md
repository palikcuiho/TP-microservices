```sh
kafka-topics --bootstrap-server localhost:9092 --create --topic book-deleted --partitions 1 --replication-factor 1
```
```sh
kafka-topics --bootstrap-server localhost:9092 --create --topic user-deleted --partitions 1 --replication-factor 1
```
```sh
kafka-topics --bootstrap-server localhost:9092 --create --topic user-locked --partitions 1 --replication-factor 1
```