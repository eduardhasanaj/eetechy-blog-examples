package catapi

import (
	"encoding/json"
	"errors"
	"fmt"
	"io"
	"net/http"
	"net/url"
	"time"
)

type CatApiClient struct {
	host       url.URL
	accessKey  string
	version    string
	httpClient *http.Client
}

var ErrInvalidHost = errors.New("cat api host address is invalid")

func NewClient(host, accessKey, v string) (*CatApiClient, error) {
	u, err := url.Parse(host)
	if err != nil {
		return nil, ErrInvalidHost
	}

	apiClient := &CatApiClient{host: *u, accessKey: accessKey, version: v}

	// create default http client which will execute requests.
	apiClient.httpClient = &http.Client{Timeout: 10 * time.Second}

	return apiClient, nil
}

func (c *CatApiClient) SearchBreeds(query url.Values) ([]*Breed, error) {
	var breeds []*Breed

	if err := c.execGet("/breeds/search", query, &breeds); err != nil {
		return nil, err
	}

	return breeds, nil
}

func (c *CatApiClient) GetBreeds(query url.Values) ([]*Breed, error) {
	var breeds []*Breed

	if err := c.execGet("/breeds", query, &breeds); err != nil {
		return nil, err
	}

	return breeds, nil
}

func (c *CatApiClient) execGet(path string, query url.Values, output interface{}) error {
	url := c.host
	url.Path = "/" + c.version + path

	fmt.Println(url.String())

	req, err := http.NewRequest(http.MethodGet, url.String(), nil)
	if err != nil {
		return err
	}

	req.Header.Add("x-api-key", c.accessKey)

	req.URL.RawQuery = query.Encode()

	resp, err := c.httpClient.Do(req)
	if err != nil {
		return err
	}
	defer resp.Body.Close()

	if resp.StatusCode != 200 {
		errBytes, _ := io.ReadAll(resp.Body)
		return errors.New(string(errBytes))
	}

	dec := json.NewDecoder(resp.Body)
	return dec.Decode(output)
}
