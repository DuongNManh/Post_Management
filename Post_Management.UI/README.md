# Post Management UI

Frontend application for the blog post management system built with Angular 19.

## Features

- Modern UI with Tailwind CSS
- Markdown support for blog posts
- Responsive design
- Dark mode support
- Image management
- Category management

## Project Structure

- `src/app/core/` - Core components and services
- `src/app/features/` - Feature modules
  - `blog-post/` - Blog post management
  - `public/` - Public-facing components
- `src/environments/` - Environment configurations

## Development

1. Install dependencies:

```bash
npm install
```

2. Start development server:

```bash
ng serve
```

3. Build for production:

```bash
ng build
```

## Testing

Run unit tests:

```bash
ng test
```

## Docker

Build the container:

```bash
docker build -t postmanagement-ui .
```

The application uses nginx for serving in production, configured in `nginx.conf`.

## Additional Resources

For more information on using the Angular CLI, including detailed command references, visit the [Angular CLI Overview and Command Reference](https://angular.dev/tools/cli) page.
