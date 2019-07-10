# To-do

* Registration Page
  * Create user models for Captain, Stoker, Volunteer, and Admin (completed in InTandemUser.cs, may need to be modified)
  * Implement User model (completed, may need to be modified)
  * Modify HTML on Register page (templating and validation complete, need to change error messages)
  * Implement server-side validation (major problems with validation right now)
  * Implement Roles system for Admin, Stoker, Captain, and Volunteer (mostly complete, roles appear to be working, )
  * Implement templating on Manage page to allow settings to be changed based on Role 
    * HTML tweaking completed 
    * proper fields now show based on role
  * Only grant user access upon email confirmation and admin confirmation
  * Refactor javascript code
    * place functions into separate modules
    * call all needed code from site.js based on page loaded
    * add event handlers for elements in js, remove event handlers from HTML
