import { CookingUnit, LengthUnit } from "./enums";
import { IIngredient } from "./ingredient"

export class Recipe {
    baseIngredientWeight: number;

    constructor(baseIngredientWeight: number) {
        this.baseIngredientWeight = baseIngredientWeight;
    }

    getRecipe() {
        // Based on one pound equaling 0.45kg and feeding four people as per https://www.thespruceeats.com/adana-kebab-4164647
        let multiplyingFactor: number = this.baseIngredientWeight / 0.45

        const ingredientsArray: IIngredient[] = [
            { name: "Base", unit: CookingUnit.Kilos, amount: this.baseIngredientWeight },
            { name: "Onion", unit: CookingUnit.Whole, amount: 1 * multiplyingFactor },
            { name: "Garlic", unit: CookingUnit.Whole, amount: 4 * multiplyingFactor },
            { name: "Cumin", unit: CookingUnit.Teaspoons, amount: 1.5 * multiplyingFactor },
            { name: "Sumac", unit: CookingUnit.Teaspoons, amount: 1.5 * multiplyingFactor },
            { name: "Salt", unit: CookingUnit.Teaspoons, amount: 0.5 * multiplyingFactor },
            { name: "Black Pepper", unit: CookingUnit.Teaspoons, amount: 0.25 * multiplyingFactor },
            { name: "Red Pepper Flakes", unit: CookingUnit.Teaspoons, amount: 0.25 * multiplyingFactor },
        ];

        return {
            weight: this.baseIngredientWeight,
            serves: 4 * multiplyingFactor,
            length: this.calculateKebabLength(),
            ingredients: ingredientsArray
        }
    }

    calculateKebabLength() {
        // https://www.saveur.com/turkish-ground-lamb-kebabs-recipe/
        // 1 lb. 6 oz = 624g
        // 10 inch skewers
        // Yield = 4
        //
        // 1 inch = 25.4cm
        //
        // 1 skewer = 156g
        // 156g = 25.4cm

        let lengthInCentimeters: number = +(((this.baseIngredientWeight * 1000) / 156) * 25.4).toFixed(2)

        if (lengthInCentimeters < 100) {
            // Return the value as centimeters
            return {
                length: lengthInCentimeters,
                unit: LengthUnit.cm
            }
        } else if (lengthInCentimeters >= 100 && lengthInCentimeters < 100000) {
            // Return the value as meters
            return {
                length: lengthInCentimeters / 100,
                unit: LengthUnit.m
            }
        } else if (lengthInCentimeters >= 100000) {
            // Return the value as kilometers
            return {
                length: lengthInCentimeters / 100000,
                unit: LengthUnit.km
            }
        }
    }
}