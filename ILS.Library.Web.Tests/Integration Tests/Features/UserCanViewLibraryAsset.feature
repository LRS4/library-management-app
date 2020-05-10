Feature: UserCanViewLibraryAsset
	In order to check out and in items
	As a library patron
	I want to view specific library items from the catalogue

Scenario: Ensure user can view a library asset's details
	Given User is at the catalogue page
	And User wants to view Plato's 'The Republic' book
	When User clicks image of book cover
	Then the book details page will display item details
