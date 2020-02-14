# gRPC Sample

Very simple demo of gRPC, communicating between .NET and Go.

## .NET steps (server)

The easiest way is to just use the templates, so doing:

```bash
dotnet new grpc
```

Will create new ASP.NET Core project with a basic gRPC service.

## .NET steps (client)

1. Create a regular console application.
2. Add packages to use gRPC

```bash
dotnet add package Google.Protobuf
dotnet add package Grpc.Net.Client
dotnet add package Grpc.Tools
```

3. Edit the `csproj` to reference the server proto file

```xml
<ItemGroup>
    <Protobuf Include="..\GrpcSample.Web\Protos\greet.proto" GrpcServices="Client" />
</ItemGroup>
```

4. Write the code

## Go steps (client)

### Setup (.NET and related I had installed, Go, not really 🙂)

1. Installed Protobuf compiler (protoc)
1. Installed Go :)
1. Set GOPATH environment variable to where the projects will be kept
1. Installed gRPC for Go `go get google.golang.org/grpc`
1. Installed the Go code generator plugin `go get -u github.com/golang/protobuf/protoc-gen-go` (it will be on the GOPATH and protoc is able to find it)

### Development

1. Create `generated` folder to put the generated code in
1. Generate code from proto file `protoc --go_out=plugins=grpc:generated --proto_path ..\..\dotnet\src\GrpcSample.Web\Protos\ greet.proto`
1. Write the code!

Test request/response client with `go run unary-client.go` and the streaming client with `go run streaming-client.go`
