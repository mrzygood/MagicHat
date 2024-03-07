### Preparing certificates

- [Install locally](https://developer.hashicorp.com/consul/docs/install)
- Add to PATH
- [Creating certificates for TLS encryption by using terminal commands](https://developer.hashicorp.com/consul/tutorials/security/tls-encryption-secure) 

```bash
cd <project-path>\vault-config\certs\consul
consul tls ca create
consul tls cert create -server -dc dc1
```

### Setting up consul

- [Instruction for running in docker compose](https://developer.hashicorp.com/consul/tutorials/docker/docker-compose-datacenter)

### Setting up Vault

Generating openssh certificate:
```bash
cd <project-path>\vault-config\certs\scripts
openssl req -x509 -newkey rsa:4096 -keyout key.pem -out cert.pem -sha256 -days 3650 -nodes -subj "/C=XX/ST=StateName/L=CityName/O=CompanyName/OU=CompanySectionName/CN=CommonNameOrHostname"
```

### Setting up Vault
```shell
docker-compose -f docker-compose.vault.yml up --build -d
```

### Vault CLI
```bash
docker exec -it vault sh
vault operator init
vault operator unseal # Execute 3 times
vault login # With root token

# Set or remove token
export VAULT_TOKEN=""
unset VAULT_TOKEN

vault audit enable file file_path=/vault/logs/audit.log
vault audit list

## KV
vault secrets enable -version=2 kv
vault kv put -mount=/kv appsettings
vault kv get -mount=/kv appsettings

## AUTH
## Create user
vault auth enable userpass
vault write auth/userpass/users/testuser1 password=Zaqwsx1@ policies=my-services # Create user with applied policy
vault login -method=userpass username=testuser1 password=<your_password> # login as specified user
vault token revoke -self # logout
```

### Consul in dev-mode without docker compose
```shell
docker run --name vault-server -d -p 8200:8200 --cap-add=IPC_LOCK -e VAULT_ADDR='http://127.0.0.1:8200' -e 'VAULT_LOCAL_CONFIG={"backend":{"consul":{"address":"host.docker.internal:8500","advertise_addr":"http://host.docker.internal", "path":"vault/"}},"listener":{"tcp":{"address":"0.0.0.0:8200","tls_disable":1}}, "ui":true}' vault:1.13.3 server
```
