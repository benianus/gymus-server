-- drop table users,persons,members,owners,employees,memberships,membership_type,membership_renewals,sessions,
--     session_types,attendances,gym_cards, products, sales, sale_items;

create table users
(
    id         int          not null primary key generated always as identity,
    username   varchar(255) not null unique,
    password   varchar(255) not null,
    role       varchar(50)  not null default 'OWNER',
    created_at date                  default current_timestamp,
    updated_at date                  default current_timestamp
);

create table persons
(
    id          int          not null primary key generated always AS IDENTITY,
    first_name  varchar(100) not null,
    last_name   varchar(100) not null,
    birthdate   date         not null,
    email       varchar(255) not null unique,
    phoneNumber varchar(25)  not null,
    address     text         not null,
    created_by  int          not null unique references users (id),
    created_at  date default current_timestamp,
    updated_at  date default current_timestamp
);

create table owners
(
    id         int not null primary key generated always as identity,
    person_id  int not null unique references persons (id),
    created_at date default current_timestamp,
    updated_at date default current_timestamp
);

create table employees
(
    id         int     not null primary key generated always as identity,
    salary     numeric not null check ( salary > 0 ),
    person_id  int     not null unique references persons (id),
    created_at date default current_timestamp,
    updated_at date default current_timestamp
);

create table members
(
    id                     int  not null primary key generated always as identity,
    birth_certificate      text not null,
    medical_certificate    text not null,
    personal_photo         text not null,
    parental_authorization text default null,
    person_id              int  not null unique references persons (id),
    created_at             date default current_timestamp,
    updated_at             date default current_timestamp
);

create table membership_type
(
    id         int          not null primary key generated always as identity,
    name       varchar(255) not null,
    price      numeric      not null check ( price > 0 ),
    created_at date default current_timestamp,
    updated_at date default current_timestamp
);

create table memberships
(
    id                 int not null primary key generated always as identity,
    member_id          int not null unique references members (id),
    owner_id           int not null references owners (id),
    start_date         date    default current_date,
    end_date           date    default (current_date + 30),
    is_active          boolean default false check ( end_date < current_date ),
    membership_type_id int not null references membership_type (id),
    created_at         date    default current_timestamp,
    updated_at         date    default current_timestamp
);

create table gym_cards
(
    id            int not null primary key generated always as identity,
    member_id     int not null references members (id) unique,
    membership_id int not null references memberships (id) unique,
    owner_id      int not null references owners (id),
    created_at    date default current_timestamp,
    updated_at    date default current_timestamp
);

create table membership_renewals
(
    id            int not null primary key generated always as identity,
    start_date    date default current_date,
    end_date      date default (current_date + 30),
    employee_id   int not null references employees (id),
    gym_card_id   int not null references gym_cards (id),
    membership_id int not null references memberships (id),
    created_at    date default current_timestamp,
    updated_at    date default current_timestamp
);

create table attendances
(
    id          int not null primary key generated always as identity,
    member_id   int not null references members (id),
    employee_id int not null references employees (id),
    created_at  date default current_timestamp,
    updated_at  date default current_timestamp
);

create table session_types
(
    id         int          not null primary key generated always as identity,
    name       varchar(100) not null,
    price      numeric      not null,
    created_at date default current_timestamp,
    updated_at date default current_timestamp
);

create table sessions
(
    id              int          not null primary key generated always as identity,
    full_name       varchar(255) not null,
    session_type_id int          not null,
    employee_id     int          not null references employees (id),
    created_at      date default current_timestamp,
    updated_at      date default current_timestamp
);

create table products
(
    id           int          not null primary key generated always as identity,
    product_name varchar(255) not null,
    quantity     int          not null,
    price        numeric      not null,
    owner_id     int          not null references owners (id),
    created_at   date default current_timestamp,
    updated_at   date default current_timestamp
);

create table sale_items
(
    id          int     not null primary key generated always as identity,
    product_id  int     not null references products (id),
    quantity    int     not null,
    total_price numeric not null check ( total_price > 0 ),
    created_at  date default current_timestamp,
    updated_at  date default current_timestamp
);

create table sales
(
    id            int not null primary key generated always as identity,
    sale_items_id int not null unique references sale_items (id),
    employee_id   int not null references employees (id),
    member_id     int null default null references members (id),
    created_at    date     default current_timestamp,
    updated_at    date     default current_timestamp
);


-- persons
-- users
-- owners
-- employees
-- members
-- memberships
-- membership_types
-- membership_renewals
-- attendances
-- sessions
-- session_types
-- gym_cards
-- products
-- sale_items
-- sales