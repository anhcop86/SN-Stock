AspnetIdentitySample
====================
<h3>What's in this sample</h3>
This is a sample which shows most of the common features of ASP.NET Identity. For more information on it, please visit http://asp.net/identity 

<h3>Running this sample</h3>
<ul>
<li>This sample uses the latest pre-release builds of ASP.NET Identity. You should be able to sync and run this sample.
</li>
</ul>

<h3>Following are the features of ASP.NET Identity in this sample</h3>
<ul>
<li>
    <b>Initialize ASP.NET Identity</b>
        You can initialize ASP.NET Identity when the application starts. Since ASP.NET Identity is Entity Framework based in this sample,
        you can create DatabaseInitializer which is configured to get called each time the app starts.
        <strong>Please look in Global.asax and App_Start\IdentityConfig.cs</strong>
        This code shows the following
        <ul>
            <li>Create user</li>
            <li>Create user with password</li>
            <li>Create Roles</li>
            <li>Add Users to Roles</li>
        </ul>
</li>
<li>
    <b>Add profile data for the user</b>
        <a href="http://blogs.msdn.com/b/webdev/archive/2013/10/16/customizing-profile-information-in-asp-net-identity-in-vs-2013-templates.aspx">Please follow this tutorial.</a>
                <ul>
                    <li>Add profile information in the Users Table</li>
                    <li>Add profile information in a different table</li>
                    <li>Look in Models\AppModel.cs for examples</li>
                </ul>
</li>
<li>
    <b>Display profile data for the user</b>
        Click Profile view profile info for the logged in user.
        For the code look in <strong>HomeController.cs Profile Action</strong>

</li>
<li>
    <b>Customize Table Name for AspNetUsers</b>
        If you want to change the default table name for the Users table, then you can do so
        by overriding the default mapping of the EF Code First types to table names.
        <strong>Look in Models\AppModel.cs on how we override the table name in ModelCreating event of DbContext</strong>
        <a href="http://msdn.microsoft.com/en-US/data/jj591617">For more info on override ModelCreating please visit</a>
</li>
<li>
<b>Claims</b>
You can store information about the user as Claims as well. This sample shows the different places where you can inject claims.
                <ul>
                    <li>Add claims to the Claims table when the User regsiters an account. Look in AccountController\Register action where I am storing Gender as a Claim</li>
                    <li>
                        Add a claim before the User Signs In. Look in AccountController\SignIn method where I am adding HomeTown as a claim. As compared to the previous case I
                        am not storing the HomeTownClaim in the database.
                    </li>
                    <li>In both these case the Claim is set on the IPrincipal when the User Signs In</li>
                </ul>
        
</li>
<li>
        <b>ClaimsIdentityFactory</b>
                When you SignIn, the UserManager creates a ClaimsIdentity by using a ClaimsIdentityFactory. This factory creates a claimsIdentity which contains Roles, UserId and UserName.
                For most apps this is sufficient. ClaimsIdentityFactory provides a central place where you can control what are the default set of claims generated for the User. In my example, let's say I do
                not care about Roles and I want to store LastLoginTime as a Claim, then I can provide my own ClaimsIdentityFactory to the UserManager.
                <ul>
                    <li>Look at Validation\MyClaimsIdentityFactory.cs for an implementation</li>
                    <li>Look at Controller\ClaimsIdentityFactoryController.cs on how we can register a ClaimsIdentityFactory with the UserManager</li>
                </ul>
       
 </li>
<li>
       <b>Validation</b>
             When you create a User using a username or password, the Identity system performs validation on the username and password, and the passwords are hashed before they are
                stored in the database. You can customize the validation by changing some of the properties of the validators such as Turn alphanumeric on/off, set minimum password length
                or you can write your own custom validators and register them with the Manager. You can use the same approach for UserManager and RoleManager.
                <ul>
                    <li>Look at Controllers\ValidationController.cs Default Action on how to tweak the default settings for the Validators</li>
                    <li>Look at IdentityExtensibility\MyValidation.cs to see how you can implement the different validators</li>
                    <li>Look at Controllers\ValidationController.cs Cutomize Action on how you can use the custom validators with the Managers</li>
                </ul>

</li>
<li>
    <b>Register a user, Login</b>
    Click Register and see the code in AccountController.cs and Register Action.
        Click Login and see the code in AccountController.cs and Login Action.
</li>
<li>
    <b>Basic Role Management</b>
    Do Create, Update, List and Delete Roles.
        Only Users In Role Admin can access this page. This uses the [Authorize] on the controller.
</li>
<li>
    <b>Basic User Management</b>
        Do Create, Update, List and Delete Users.
        Assign a Role to a User.
        Only Users In Role Admin can access this page. This uses the [Authorize] on the controller.
</li>
<li>
    <b>Associating ToDoes with User</b>
        This example shows how you can create a ToDo application where you can associate ToDoes with a User.
        Following are the salient features of this sample.
        <ul>
            <li>Create ToDo model and associate User in EF Code First. Goto Models\AppModel.cs </li>
            <li>Only Authenticated Users can Create ToDo</li>
            <li>When you create/list ToDo, we can filter by User. Look at ToDoController</li>
            <li>Only Users in Role Admin can see all ToDoes. Look at ToDoController and All action.</li>
        </ul>
</li>
<ul>
v1.0.0-RTM
-----------
Following are the features in this project. https://github.com/rustd/AspnetIdentitySample/commit/776680f37657affff109a1107c90cde4963d2eb2 has the list of changes as well the code to change to migrate from v1.0-RC1 to v1.0-RTM
- Initialize ASP.NET in App_Start by creating an Admin user and add the user to Admin Role
- Basic Role Management which is restricted to Users in Admin Roles Only. 
Admin can create, update, delete (remove all users from this role) roles and view the details of the role (Users in this role).
You can look at the RolesAdmin Controller
- Basic User Management  which is restricted to Users in Admin Roles Only. 
Admin can create user (add a user to role as well), edit user details(such as profile data and modify the roles for the user).
You can look at the UsersAdmin Controller


V1.0.0-RC1
-----------
Following are the features in this project. https://github.com/rustd/AspnetIdentitySample/commit/3738ae8a36bf8ad568e4593c6cd3174e6af6ed41 has the list of changes
- Initialize ASP.NET in App_Start by creating an Admin user and add the user to Admin Role
- Basic Role Management which is restricted to Users in Admin Roles Only. 
Admin can create, update, delete (remove all users from this role) roles and view the details of the role (Users in this role).
You can look at the RolesAdmin Controller
- Basic User Management  which is restricted to Users in Admin Roles Only. 
Admin can create user (add a user to role as well), edit user details(such as profile data and modify the roles for the user).
You can look at the UsersAdmin Controller


V1.0.0-Beta1
-----------
Following are the steps to customize profile. Note once you do this then Basic User and Role Management will not work
- Open the solution
- Build and run
- Regsiter a user
---- Notice that the user registration field only has user name and password
- Let's ask for a birthdate option from the user while registering an account.
- Goto Nuget Package Manager console and run "Enable-Migrations"
- Goto Models\AppModel.cs and uncomment BirthDate property in the MyUser class
- Goto Models\AccountViewModels.cs and uncomment BirthDate property in RegisterViewModel
- Goto AccountController and in Register Action and have the following code
          var user = new MyUser() { UserName = model.UserName,BirthDate=model.BirthDate };
          //var user = new MyUser() { UserName = model.UserName };
- Goto Views\Account\Register.cshtml and uncomment the html markup to add a BirthDate column
- Goto Nuget Package Manager console and run "Add-Migration BirthDate"
- Goto Nuget Package Manager console and run "Update-Database"
- Run the application
- When you register a user then you can enter BirthDate as well 
