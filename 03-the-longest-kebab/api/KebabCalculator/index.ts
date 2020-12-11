import { AzureFunction, Context, HttpRequest } from "@azure/functions"

import { Recipe } from "./recipe"

const httpTrigger: AzureFunction = async function (context: Context, req: HttpRequest): Promise<void> {
    context.log('HTTP trigger function processed a request.');
    const baseIngredientWeight = (req.query.baseIngredientWeight || (req.body && req.body.baseIngredientWeight));

    if (isNumeric(baseIngredientWeight)) {
        let recipt = new Recipe(baseIngredientWeight);

        context.res = {
            // status: 200, /* Defaults to 200 */
            body: recipt.getRecipe()
        };
    } else {
        context.res = {
            status: 400,
            body: "Please provide the weight of your base ingredient as a number."
        }
    }

};

function isNumeric(baseIngredientWeight) {
    return !isNaN(baseIngredientWeight)
}

export default httpTrigger;