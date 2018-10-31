Feature: Health
	In order to determine if an API instance is running without catastropic errors
	As an automated poller
	I want a quick view of the API health

Scenario: GET health
	Given the API is running
	When I request GET health
	Then the response should contain "Healthy"
