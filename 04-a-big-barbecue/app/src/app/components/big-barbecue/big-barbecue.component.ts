import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormArray,
  FormControl,
  Validators,
  FormGroup,
} from '@angular/forms';

import { BarbecueCostService } from 'src/app/services/barbecue-cost.service';
import { BarbequeCostResult } from './barbeque-cost-result';

@Component({
  selector: 'app-big-barbecue',
  templateUrl: './big-barbecue.component.html',
  styleUrls: ['./big-barbecue.component.css'],
})
export class BigBarbecueComponent implements OnInit {
  barbequeForm;
  barbecueItemGroups: FormArray;
  barbecueCostResult: BarbequeCostResult;

  loading: boolean;
  errors: string;

  constructor(
    private formBuilder: FormBuilder,
    private barbecueCostService: BarbecueCostService
  ) {}

  ngOnInit(): void {
    this.barbecueItemGroups = this.formBuilder.array(
      this.getBarbecueItems().map((barbecueItem) =>
        this.formBuilder.group(barbecueItem)
      )
    );

    this.barbequeForm = this.formBuilder.group({
      name: ['', Validators.required ],
      guests: [, [Validators.required, Validators.pattern("^[0-9]*$")] ],
      budget: [, [Validators.required, Validators.pattern("[0-9]+(\.[0-9][0-9]?)?")] ],
      barbecueItems: this.barbecueItemGroups,
    });
  }

  getBarbecueItems() {
    const barbecueItemsControlArray = [];

    barbecueItemsControlArray.push({
      name: 'Beef',
      cost: [12, Validators.required ],
      quantity: [1, Validators.required ],
      quantityUnit: 'kg',
    });
    barbecueItemsControlArray.push({
      name: 'Fillet',
      cost: 32.77,
      quantity: 1,
      quantityUnit: 'kg',
    });
    barbecueItemsControlArray.push({
      name: 'Sausages',
      cost: 12.59,
      quantity: 1,
      quantityUnit: 'kg',
    });
    barbecueItemsControlArray.push({
      name: 'Grilled Cheese',
      cost: 1.15,
      quantity: 1,
      quantityUnit: 'pcs',
    });
    barbecueItemsControlArray.push({
      name: 'Garlic Bread',
      cost: 1.25,
      quantity: 1,
      quantityUnit: 'pcs',
    });
    barbecueItemsControlArray.push({
      name: 'Chicken',
      cost: 5,
      quantity: 1,
      quantityUnit: 'kg',
    });

    return barbecueItemsControlArray;
  }

  onSubmit(formData) {
    if (this.barbequeForm.valid) {
      this.barbecueCostResult = null;
      this.errors = null;
      this.loading = true;

      const barbecueCostResult = this.barbecueCostService
      .getBarbecueCosting(formData)
      .subscribe(
        (barbecueCostResult) => {
          this.barbecueCostResult = barbecueCostResult;
        },
        (error) => {
          this.errors = error;
          this.loading = false;
        },
        () => {
          this.loading = false;
        }
      );
    } else {
      this.validateAllFormFields(this.barbequeForm);
    }

  }

  isFieldValid(field: string) {
    return !this.barbequeForm.get(field).valid && this.barbequeForm.get(field).touched;
  }

  displayFieldCss(field: string) {
    return {
      'has-error': this.isFieldValid(field),
      'has-feedback': this.isFieldValid(field)
    };
  }

  get employees(): FormArray {
    return this.barbequeForm.get('barbecueItems') as FormArray;
  }

  validateAllFormFields(formGroup: FormGroup) {
  Object.keys(formGroup.controls).forEach(field => {
    const control = formGroup.get(field);

    if (control instanceof FormControl) {
      control.markAsTouched({ onlySelf: true });
    } else if (control instanceof FormGroup) {
      this.validateAllFormFields(control);
    }});
  }
}
