@authentication
Feature: Recover password
    In order to recover forgotten Password or UserName
    As a player
    I want to receive my credentials via my registered UserEmail

    Background:
        Given I am at RecoverPassword page
        
    Scenario: Successful password recovery
        Given I entered valid UserEmail
        And entered UserEmail registered
        When I submit PasswordRecovery form
        Then my credentials sent to my UserEmail
        And I am redirected to WelcomePage

    Scenario: UserEmail is not valid 
        Given entered UserEmail is not valid
        When I submit PasswordRecovery form
        Then I am redirected to RecoveryPassword page
        And ErrorMessage shows that UserEmail is not correct
        And UserEmail textbox contains entered invalid UserEmail

    Scenario: UserEmail was not registered
        Given entered UserEmail is not valid
        When I submit PasswordRecovery form
        Then I am redirected to RecoveryPassword page
        And ErrorMessage shows that UserEmail is not correct
        And UserEmail textbox contains entered invalid UserEmail
