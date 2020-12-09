import { CookingUnit, LengthUnit } from "./enums"

class KebabRecipe {
    weight: string
    serves: string
    length: {
        length: string
        unit: LengthUnit
    }
    ingredients: [{
        name: string
        unit: CookingUnit
        amount: string
    }]
}

export { KebabRecipe }