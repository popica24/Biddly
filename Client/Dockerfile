FROM node:18-alpine as build

WORKDIR /app

COPY package*.json .

RUN npm install

COPY . .

ENV VITE_API_LOCAL_URL = https://localhost:7174/api

RUN npm run build

FROM nginx:alpine as prod

COPY --from=build /app/dist /usr/share/nginx/html/public
COPY default.conf /etc/nginx/conf.d/default.conf

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]