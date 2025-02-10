import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { NgClass, NgIf } from '@angular/common';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterLink, RouterLinkActive, NgClass, NgIf],
  templateUrl: './navbar.component.html'
})
export class NavbarComponent {
  isMenuOpen = false;
  isDropdownOpen = false;

  toggleTheme() {
    const currentTheme = document.body.classList.toggle('dark');
    localStorage.setItem('theme', currentTheme ? 'dark' : 'light');
  }

  ngOnInit() {
    const savedTheme = localStorage.getItem('theme');
    if (savedTheme === 'dark') {
      document.body.classList.add('dark');
    }
  }
}