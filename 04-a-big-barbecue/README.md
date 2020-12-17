![banner](assets/banner-4.png)

# Challenge 4: A Big Barbecue!

## Solution
The solution contains a Angular frontend to allow users to input the details of their barbecue, and an Azure Function as the backend to calculate the costs and how the spend compared to the budget.

The BBQ organiser can input the following details:
* Their name
* Number of guests
* Budget
* The items for their barbeque, cost per kg/pcs and the quantity

Some example prices have been pre-filled.

Using the spreadsheet as a guide and a bit of artistic license, the Azure Function will then calculate the following:
* Total cost of the items
* Cost per guest
* Over or under budget

The results then get displayed back to the user. They can then alter the quantities and cost of the items to see how the overall cost is affected.

Both the front and backend are then hosted in an Azure Static Web app.

## Try it yourself
You can try out a working version deployed to an Azure Static Web App [here](http://www.bigbarbecue.cloud).

![the-longest-kebab](assets/a-big-barbeque.png)

You can also run the Azure Function on it's own. Just start running it in your favourite IDE, or deploy to an Azure Function app. Just create a `POST` request with the same body that the UI would send. For example:

`http://localhost:7071/api/barbecuecost`

```json
{
   "name":"Stuart",
   "guests":10,
   "budget":"175",
   "barbecueItems":[
      {
         "name":"Beef",
         "cost":12,
         "quantity":"3",
         "quantityUnit":"kg"
      },
      {
         "name":"Fillet",
         "cost":32.77,
         "quantity":"2",
         "quantityUnit":"kg"
      },
      {
         "name":"Sausages",
         "cost":12.59,
         "quantity":"2",
         "quantityUnit":"kg"
      },
      {
         "name":"Grilled Cheese",
         "cost":1.15,
         "quantity":"10",
         "quantityUnit":"pcs"
      },
      {
         "name":"Garlic Bread",
         "cost":1.25,
         "quantity":"10",
         "quantityUnit":"pcs"
      },
      {
         "name":"Chicken",
         "cost":5,
         "quantity":"1.5",
         "quantityUnit":"kg"
      }
   ]
}
```

![postman](assets/postman.png)

# The Challenge

## Your Chefs: Lucas Santos, Cloud Advocate, Microsoft, with Rogerio Rodrigues and Sydney Cirqueira, Microsoft Student Ambassadors, with special help from Lucas's Super-BBQ-Chef father

## This week's featured region: Brazil

In Brazil, everyone loves a big barbecue! Brazilian barbecue is justifiably famous. Everyone's friends and family gets together for a feast, complete with grilled meats called churrasco, typically beef with different marinades. You can also find prime rib, linguica (a kind of sausage), lamb kebabs, chicken legs, fish, and much more.

Some families have created whole systems to help organize the amount of food needed for the number of people invited to a big barbecue. We have gained access to a spreadsheet used to organize large family barbecues: [here it is](assets/bbq-spreadsheet.xlsx).

Big events like this can get expensive, and you need to stay in within your budget. The spreadsheet used here enables you to determine how many people will attend the barbecue, how much food will be needed, and how much it will cost.

## Your challenge 🍽

Using the percentages in the spreadsheet, create a serverless method of determining how many people you can invite based on a given budget (you can determine your budget and currency). Can you invite more people if you serve less fillet and more grilled cheese? Show us a good solution to create a really big BBQ!

## Resources/Tools Used 🚀

-   **[Visual Studio Code](https://code.visualstudio.com/?WT.mc_id=academic-10922-cxa)**
-   **[Postman](https://www.getpostman.com/downloads/)**
-   **[Azure Functions Extension](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-azurefunctions&WT.mc_id=academic-10922-cxa)**

## Next Steps 🏃

Learn more about serverless!

  ✅ **[Serverless Free Courses](https://docs.microsoft.com/learn/browse/?term=azure%20functions&WT.mc_id=academic-10922-cxa)**

## Important Resources ⭐️

  ✅ **[Azure Functions documentation](https://docs.microsoft.com/azure/azure-functions/?WT.mc_id=academic-10922-cxa)**

  ✅ **[Azure SDK for JavaScript Documentation](https://docs.microsoft.com/azure/javascript/?WT.mc_id=academic-10922-cxa)**

  ✅ **[Create your first function using Visual Studio Code](https://docs.microsoft.com/azure/azure-functions/functions-create-first-function-vs-code?WT.mc_id=academic-10922-cxa)**

  ✅ **[Free E-Book - Azure Serverless Computing Cookbook, Second Edition](https://azure.microsoft.com/resources/azure-serverless-computing-cookbook/?WT.mc_id=academic-10922-cxa)**

## Ready to submit a solution to this challenge? Here's how 🚀

Open an [issue](https://github.com/microsoft/Seasons-of-Serverless/issues/new?assignees=&labels=&template=seasons-of-serverless-solution.md&title=Solution) in this repo, with a link to your challenge and a brief explanation of how you solved it. We will take a look, approve it if appropriate, and a tag with the appropriate week. If your solution is picked as a weekly standout solution, we'll send you a little prize!
## Credits

<a href="https://www.vecteezy.com/free-vector/meal">Meal Vectors by Vecteezy</a>
