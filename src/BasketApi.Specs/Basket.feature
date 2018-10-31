Feature: Basket
	In order to keep track of my items to purchase
	As a customer
	I want to manage my basket

Scenario: GET basket
	Given the API is running
	When I request GET basket for my basket ID
	Then the response should contain "DefaultItem"
