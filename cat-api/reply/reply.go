package reply

import (
	"encoding/json"
	"net/http"
)

// Reply to the request by sending given data
func With(w http.ResponseWriter, data interface{}) {
	resp := make(map[string]interface{})

	resp["code"] = 0
	resp["data"] = data

	write(w, resp)
}

// Reply to the request by sending error
// Errors can be about validation or internal
func WithError(w http.ResponseWriter, err error) {
	resp := make(map[string]interface{})

	resp["error"] = err.Error()

	write(w, resp)
}

// Write response data by serializing it in json format
func write(w http.ResponseWriter, payload interface{}) {
	w.Header().Set("content-type", "application/json")
	enc := json.NewEncoder(w)

	if err := enc.Encode(payload); err != nil {
		// needs log the error and investigate
	}
}
