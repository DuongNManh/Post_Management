import { Routes } from '@angular/router';
import { CategoryListComponent } from './features/category/category-list/category-list.component';
import { AddCategoryComponent } from './features/category/add-category/add-category.component';
import { EditCategoryComponent } from './features/category/edit-category/edit-category.component';
import { BlogpostListComponent } from './features/blog-post/blogpost-list/blogpost-list.component';
import { AddBlogpostComponent } from './features/blog-post/add-blogpost/add-blogpost.component';
import { HomeComponent } from './features/public/home/home.component';
import { BlogDetailComponent } from './features/public/blog-detail/blog-detail.component';

export const routes: Routes = [
    {
        path: 'admin/categories',
        component: CategoryListComponent
    },
    {
        path: 'admin/categories/add',
        component: AddCategoryComponent
    },
    {
        path: 'admin/categories/edit/:id',
        component: EditCategoryComponent
    },
    {
        path: 'admin/blog-posts',
        component: BlogpostListComponent
    },
    {
        path: 'admin/blog-posts/add',
        component: AddBlogpostComponent
    },
    {
        path: 'admin/blog-posts/edit/:id',
        component: AddBlogpostComponent
    },
    {
        path: '',
        component: HomeComponent
    },
    {
        path: 'blog/:url',
        component: BlogDetailComponent
    }

];
