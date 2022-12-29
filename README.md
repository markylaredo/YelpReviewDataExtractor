# Yelp Reviews API Endpoint

This API endpoint allows you to retrieve reviews for a specific restaurant on Yelp and process the review data using the Yelp API. The endpoint takes a parameter that specifies the ID of the restaurant, and returns a list of reviews in JSON format.


For each review, the API endpoint also processes the reviewer's avatar image using the Google Vision API to extract emotions data such as ``joyLikelihood`` and ``sorrowLikelihood``. This data is included in the JSON output along with the review information.
