import { Directive, ElementRef, HostListener, Input, Renderer2, SimpleChanges } from '@angular/core';

@Directive({
  selector: '[appScrolling]'
})
export class ScrollingDirective {
  scrolHight: number;

  constructor(private  renderer2:Renderer2, private el: ElementRef) {}

 ngAfterViewInit(): void {


   this.changePosition('bottom');

 }

 @Input('appScrolling') position: string


ngOnChanges(changes: SimpleChanges): void {
  if(changes.position){
    this.scrolHight = this.el.nativeElement.scrollHeight;
    this.changePosition(this.position);
  }
}



 private changePosition(position: string){
    if(position === 'top'){
      this.el.nativeElement.scrollTop = 0;
    }

    if(position === 'bottom'){
      this.el.nativeElement.scrollTop = this.el.nativeElement.scrollHeight;
    }
 }
}
