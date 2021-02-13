# Sales-Receipt-Application
Sales receipt application processes:
1. Accepts line by line texts as input,
2. Sanitize input strings by checking if 
input order is the same as exected E.g 
1 Food product 23.00. It will as well 
calls a function to remove extra white spaces
3. Calls a function to process sanitized
input into objects
4. Get category of each product by calling 
GetProductCategory function. Category are grouped 
by Essentials, NonEssentials, ImportedGeneral
and ImportedEssentials
5. Calculate imported and sales taxes based on
category of product and round taxes to 5cent
6. Create a list object as container to hold
processed items, if item already exists in the 
container, it will update the object
7. Finally appends sales taxes and total to make the 
last two lines and print receipt into UI control
8. Also created unit test for all functions using
MSTest framework 
