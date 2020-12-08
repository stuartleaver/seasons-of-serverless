import { Unit } from "./enums";
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
            { name: "Base", unit: Unit.Kilos, amount: this.baseIngredientWeight },
            { name: "Onion", unit: Unit.Whole, amount: 1 * multiplyingFactor },
            { name: "Garlic", unit: Unit.Whole, amount: 4 * multiplyingFactor },
            { name: "Cumin", unit: Unit.Teaspoons, amount: 1.5 * multiplyingFactor },
            { name: "Sumac", unit: Unit.Teaspoons, amount: 1.5 * multiplyingFactor },
            { name: "Salt", unit: Unit.Teaspoons, amount: 0.5 * multiplyingFactor },
            { name: "Black Pepper", unit: Unit.Teaspoons, amount: 0.25 * multiplyingFactor },
            { name: "Red Pepper Flakes", unit: Unit.Teaspoons, amount: 0.25 * multiplyingFactor },
        ];

        return {
            weight: this.baseIngredientWeight,
            serves: 4 * multiplyingFactor,
            ingredients: ingredientsArray
        }
    }
}