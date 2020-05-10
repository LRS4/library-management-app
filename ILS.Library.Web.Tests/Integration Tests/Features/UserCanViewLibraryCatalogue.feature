Feature: UserCanViewLibraryCatalogue
	In order to check in and check out books and videos etc
	As a library patron
	I want to view the library catalogue 

Scenario: Ensure user can view library catalogue
	Given User is at the landing page
	And User clicks the start now button
	When User is at library catalogue page
	Then the catalogue should be displayed on the screen
