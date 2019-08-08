# To-do

* Registration Page
  * Create user models for Captain, Stoker, Volunteer, and Admin (completed in InTandemUser.cs, may need to be modified)
  * Implement User model (completed, may need to be modified)
  * Modify HTML on Register page (templating and validation complete, need to change error messages)
    * templating (complete)
    * client-side validation (complete)
  * Implement server-side validation (major problems with validation right now)
  * Implement Roles system for Admin, Stoker, Captain, and Volunteer (mostly complete, roles appear to be working, )
  * Implement templating on Manage page to allow settings to be changed based on Role 
    * HTML tweaking completed 
    * proper fields now shown based on role
  * Only grant user access upon email confirmation and admin confirmation
    * email confirmation needs debugging (complete)
    * basic email confirmation system implemented (complete)
    * need to send from custom email that is not joe@contoso.com (done, will need to be changed prior to deployment)
  * require approval from admins 
    * display table of all users (done)
    * display detials of selected user (done, will need to show events)
    * approval and denial funcitonality added
  * Refactor javascript code
    * place functions into separate modules
    * call all needed code from site.js based on page loaded
    * add event handlers for elements in js, remove event handlers from HTML (done)
    * clear and repopulate inputs based on user selection on Manage and Registration page (debugging needed)
  * events page
    * cross table created and records registrations, stores user and event id
    * need to categorize and show events based on status
    * need to tweak links shown based on if user is signed in
  * miscellaneous changes
    * disable two factor authentication (done)
    * implement authorization on events pages (need to transfer over auth handler code from avnish's repo)
    * change navbar links
    * prompt for phone number and years of experience on register page 
    * show expertise on users page for admins
    * allow verification email to be resent in case of error (done)
    * add reason why event was cancelled (added to user model)
    * add field for captains to say if they came from NY Cares (done)
