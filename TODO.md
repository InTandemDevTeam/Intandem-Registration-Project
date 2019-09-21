# To-do

* User Management
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
  * miscellaneous changes
    * disable two factor authentication (done)
    * change navbar links
    * prompt for phone number and years of experience on register page (done)
    * show expertise on users page for admins
    * allow verification email to be resent in case of error (done)
    * add field for captains to say if they came from NY Cares (done)
    * made Users table column names dynamic (done)
    * removed duplicate DbContext (done)
    * made name of fields on user details page dynamic (done)
    * allow user to input and change username (done)
    * users are sorted into different lists based on if their approval is pending or not
    * implement searching, paging, sorting, and filtering for users page
* Events 
  * cross table created and records registrations, stores user and event id (done)
  * need to categorize and show events based on status (done)
  * need to tweak links shown based on if user is signed in (done)
  * add reason why event was cancelled (done)  
  * implement authorization on events pages (seems to be working)
  * show list of ride leaders (done)
  * give ride leaders ability to edit event (done)
  * implement assigning multiple ride leaders (pending)
  * implement paging, sorting, searching, and filtering on events page
  * implement adding/changing ride leaders after event creation (done)
  * make sure selecting and viewing selection of multiple ride leaders is accessible
  * implement wizard functionality for event creation (done, pending refinement)
  * erase certain fields for event creation and editing
  * implement search for selecting ride leaders
  * replace text box with dropdown for MaxSignUpType, Status, and EventType (done, now uses enum instead of string for type)
  * add dropdown for Status, MaxSignUpType, and EventType on Edit page (done)
  * added ability to copy event (done)
  * implemented basic search by full name on users page (done)
