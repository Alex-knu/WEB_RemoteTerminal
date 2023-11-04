FROM node:slim as node
WORKDIR /app
COPY ./ /app
RUN npm install --force
RUN npm run build --prod

FROM nginx:latest
COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=node /app/dist/angular-app /usr/share/nginx/html