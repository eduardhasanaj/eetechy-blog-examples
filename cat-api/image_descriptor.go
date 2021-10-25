package catapi

type ImageDescriptor struct {
	Height int    `json:"height"`
	ID     string `json:"id"`
	URL    string `json:"url"`
	Width  int    `json:"width"`
}
