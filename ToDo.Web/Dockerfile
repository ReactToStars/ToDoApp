FROM node:22-alpine AS build
WORKDIR /app

COPY ToDo.Web/package*.json ./
RUN npm ci

COPY ToDo.Web/ ./
RUN npm run build -- --output-path=dist/app

FROM nginx:alpine AS final
COPY ToDo.Web/nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /app/dist/app/browser /usr/share/nginx/html
EXPOSE 80
