Feature: Basket
	In order to keep track of my items to purchase
	As a customer
	I want to manage my basket

Scenario: Create a basket
	Given the API is running
	When I POST a basket without basket ID
	Then the response should contain my new basket ID
    And my new basket should be saved

Scenario: Get a basket
	Given the API is running
    And I have a basket
	When I GET my basket
	Then the response should contain my basket

Scenario: Add item to a basket
	Given the API is running
    And I have a basket
	When I PUT a "Banana" item in to my basket
	Then my basket should have the "Banana" item

Scenario: Update item quantity
	Given the API is running
    And I have a basket with a "Banana" item
	When I PUT my "Banana" item as a new quantity in my basket
	Then my basket should have the new "Banana" quantity