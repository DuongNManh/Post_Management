import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { BlogPostService } from '../../blog-post/services/blog-post.service';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../../core/models/api-response';
import { BlogPostModel } from '../../blog-post/models/BlogPostModel';
import { NgIf, NgFor, CommonModule } from '@angular/common';

@Component({
  selector: 'app-blog-detail',
  standalone: true,
  imports: [NgIf, NgFor, CommonModule, RouterModule],
  templateUrl: './blog-detail.component.html',
  styleUrls: ['./blog-detail.component.css']
})
export class BlogDetailComponent implements OnInit {
  url: string | null = null;
  blogPost?: BlogPostModel;

  constructor(private route: ActivatedRoute, private blogPostService: BlogPostService) { }
  
  ngOnInit(): void {
    this.route.params.subscribe({
      next: params => {
        this.url = params['url'];
        if (this.url) {
          this.blogPostService.getBlogPostByUrl(this.url)
            .subscribe(response => {
              if (response.is_success) {
                this.blogPost = response.data;
                console.log(this.blogPost);
              }
            })
        }
      }
    });
  }
  // fetch blog detail by url
}
