Hello! This is my project for a numismatic web app that focuses on a user login/register on the website and choose whatever coins exist on the website database to their own collection. It also lets you export your collection via PDF file. 
You can expect the standard actions regarding user account management. Also a facilitated way to search coins so you do not have to be roaming around 10 pages of coins with 15 coins each page.

Any kind of improvement you have you can pull and commit and will be further evaluated and merged if aplicable.

Thank you and enjoy!


Directions:

All the functions used project-wide are inside the "Classes" folder. Ocasional encrypt or decrypt functions are also on some aspx.cs files but are less important.

The code has comments where I think it is needed for further interpretation.

The requirements of the project where the following:

    - Basic Login/Register and incorporate Google Sign In as well
    - Account activation system via email. Login only possible if the account is validated through a link sent via email.
    - Coint catalog where the user would be able to search based on several filters (name, type, sort via value, etc).
    - System where the user would be able to add to his own collection any coin he sees on the catalog. Also a way to consult his own collection and export it via PDF aswell.
    - The user should be able to at least change his own password and even retrieve his own password if needed via email where the system generates and delivers via email the new password. In this project I also added changing email, username and the hability to delete own account.
    - The admin area should make posible to manage the coins inserted in the system, delete them if needed. In this case I opted for not deleting but deactivating the coins and also edit them. Same for the users.
    - Dashboard page where the admin has access to some statistics based on the database connected to the website.