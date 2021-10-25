package main

import (
	"net/http"

	"github.com/eduarhasanaj/catapi"
	"github.com/eduarhasanaj/catapi/reply"
)

var client *catapi.CatApiClient

func main() {
	c, err := catapi.NewClient("https://api.thecatapi.com", "bd97f8bb-19d8-404c-8dfc-b7afd6a0b6a2", "v1")

	if err != nil {
		panic(err)
	}

	client = c

	http.HandleFunc("/api/breeds", handleGetBreeds)

	http.HandleFunc("/api/breeds/search", handleSeachBreeds)

	http.ListenAndServe(":4000", nil)
}

func handleSeachBreeds(w http.ResponseWriter, r *http.Request) {
	breeds, err := client.SearchBreeds(r.URL.Query())
	if err != nil {
		reply.WithError(w, err)
		return
	}

	reply.With(w, breeds)
}

func handleGetBreeds(w http.ResponseWriter, r *http.Request) {
	breeds, err := client.GetBreeds(r.URL.Query())
	if err != nil {
		reply.WithError(w, err)
		return
	}

	reply.With(w, breeds)
}
