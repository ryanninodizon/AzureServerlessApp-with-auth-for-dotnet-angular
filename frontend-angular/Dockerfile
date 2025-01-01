FROM node:latest AS build
WORKDIR /app
COPY package*.json ./

RUN npm ci

RUN npm install -g @angular/cli

COPY . .

RUN npm install
RUN ng build

FROM nginx:latest

COPY ./nginx.conf /etc/nginx/conf.d/default.conf

COPY --from=build /app/dist/frontend-angular/browser /usr/share/nginx/html

expose 80
#docker build -t angular-with-msal-template .
#run: docker run -d -p 8080:80 angular-with-msal-template
