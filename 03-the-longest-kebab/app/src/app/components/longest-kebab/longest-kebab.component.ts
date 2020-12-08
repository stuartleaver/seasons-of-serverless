import { Component, OnInit } from '@angular/core';

import { KebabReciptService} from 'src/app/services/kebab-recipt.service';

@Component({
  selector: 'app-longest-kebab',
  templateUrl: './longest-kebab.component.html',
  styleUrls: ['./longest-kebab.component.css']
})
export class LongestKebabComponent implements OnInit {

  constructor(private kebabReciptService: KebabReciptService) { }

  ngOnInit(): void {
  }

  private getKebabRecipt() {
    const kebabRecipt = this.kebabReciptService.getKebabRecipt();
  }

}
