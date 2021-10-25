package catapi

type Breed struct {
	Adaptability     int             `json:"adaptability"`
	AffectionLevel   int             `json:"affection_level"`
	AltNames         string          `json:"alt_names"`
	CfaURL           string          `json:"cfa_url"`
	ChildFriendly    int             `json:"child_friendly"`
	CountryCode      string          `json:"country_code"`
	CountryCodes     string          `json:"country_codes"`
	Description      string          `json:"description"`
	DogFriendly      int             `json:"dog_friendly"`
	EnergyLevel      int             `json:"energy_level"`
	Experimental     int             `json:"experimental"`
	Grooming         int             `json:"grooming"`
	Hairless         int             `json:"hairless"`
	HealthIssues     int             `json:"health_issues"`
	Hypoallergenic   int             `json:"hypoallergenic"`
	ID               string          `json:"id"`
	Image            ImageDescriptor `json:"image"`
	Indoor           int             `json:"indoor"`
	Intelligence     int             `json:"intelligence"`
	Lap              int             `json:"lap"`
	LifeSpan         string          `json:"life_span"`
	Name             string          `json:"name"`
	Natural          int             `json:"natural"`
	Origin           string          `json:"origin"`
	Rare             int             `json:"rare"`
	ReferenceImageID string          `json:"reference_image_id"`
	Rex              int             `json:"rex"`
	SheddingLevel    int             `json:"shedding_level"`
	ShortLegs        int             `json:"short_legs"`
	SocialNeeds      int             `json:"social_needs"`
	StrangerFriendly int             `json:"stranger_friendly"`
	SuppressedTail   int             `json:"suppressed_tail"`
	Temperament      string          `json:"temperament"`
	VcahospitalsURL  string          `json:"vcahospitals_url"`
	VetstreetURL     string          `json:"vetstreet_url"`
	Vocalisation     int             `json:"vocalisation"`
	Weight           struct {
		Imperial string `json:"imperial"`
		Metric   string `json:"metric"`
	} `json:"weight"`
	WikipediaURL string `json:"wikipedia_url"`
}
