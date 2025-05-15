import { Component } from '@angular/core';
import { MovimentoManualComponent } from './movimento-manual/movimento-manual.component';

@Component({
    selector: 'app-root',
    standalone: true,
    imports: [MovimentoManualComponent],
    template: `
    <app-movimento-manual></app-movimento-manual>
  `,
})
export class AppComponent {
    title = 'Movimentos Manuais';
}