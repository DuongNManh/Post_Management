import { Component, OnInit } from '@angular/core';
import { BlogPostService } from '../../blog-post/services/blog-post.service';
import { BlogPostModel } from '../../blog-post/models/BlogPostModel';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../../core/models/api-response';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterModule } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    RouterModule
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  constructor(private blogPostService: BlogPostService) {}
  blogPosts$?: Observable<ApiResponse<BlogPostModel[]>>;

  ngOnInit(): void {
    this.blogPosts$ = this.blogPostService.getAllBlogPosts();
  }

  goToPost(id: string) {
  }

}
