package main

import (
	"io"
	"log"
	"os"

	pb "generated"

	"golang.org/x/net/context"
	"google.golang.org/grpc"
)

const (
	address     = "localhost:5000"
	defaultName = "Streaming Server Client"
)

func main() {
	// Set up a connection to the server.
	conn, err := grpc.Dial(address, grpc.WithInsecure())
	if err != nil {
		log.Fatalf("did not connect: %v", err)
	}
	defer conn.Close()
	c := pb.NewGreeterClient(conn)

	// Contact the server and print out its response.
	name := defaultName
	if len(os.Args) > 1 {
		name = os.Args[1]
	}
	stream, err := c.LotsOfReplies(context.Background(), &pb.HelloRequest{Name: name})
	if err != nil {
		log.Fatalf("could not greet: %v", err)
	}
	for {
		reply, err := stream.Recv()
		if err == io.EOF {
			break
		}
		if err != nil {
			log.Fatalf("could not stream greets: %v", err)
		}
		log.Println(reply.Message)
	}
}
