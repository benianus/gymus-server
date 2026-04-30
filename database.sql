create table users
(
    id         int          not null primary key generated always as identity,
    username   varchar(255) not null unique,
    password   varchar(255) not null,
    role       varchar(50)  not null default 'OWNER',
    created_at timestamp(2)          default current_timestamp(2),
    updated_at timestamp(2)          default current_timestamp(2)
);

create table members
(
    id          int          not null primary key generated always AS IDENTITY,
    first_name  varchar(100) not null,
    last_name   varchar(100) not null,
    birthdate   date         not null,
    email       varchar(255) not null unique,
    phoneNumber varchar(25)  not null unique,
    address     text         not null,
    created_by  int          not null references users (id),
    created_at  timestamp(2) default current_timestamp(2),
    updated_at  timestamp(2) default current_timestamp(2)
);

create table member_documents
(
    id                     int  not null primary key generated always as identity,
    birth_certificate      text not null,
    medical_certificate    text not null,
    personal_photo         text not null,
    parental_authorization text         default null,
    member_id              int  not null unique references members (id),
    added_by               int  not null references users (id),
    created_at             timestamp(2) default current_timestamp(2),
    updated_at             timestamp(2) default current_timestamp(2)
);

create table membership_type
(
    id         int          not null primary key generated always as identity,
    name       varchar(255) not null,
    price      numeric      not null check ( price > 0 ),
    created_at timestamp(2) default current_timestamp(2),
    updated_at timestamp(2) default current_timestamp(2)
);

create table memberships
(
    id                 int not null primary key generated always as identity,
    member_id          int not null unique references members (id),
    start_date         date         default current_date,
    end_date           date         default (current_date + 30),
    membership_type_id int not null references membership_type (id),
    created_by         int not null references users (id),
    created_at         timestamp(2) default current_timestamp(2),
    updated_at         timestamp(2) default current_timestamp(2)
);

create table members_cards
(
    id            int  not null primary key generated always as identity,
    join_date     date not null default current_date,
    member_id     int  not null references members (id) unique,
    membership_id int  not null references memberships (id) unique,
    created_by    int  not null references users (id),
    created_at    timestamp(2)  default current_timestamp(2),
    updated_at    timestamp(2)  default current_timestamp(2)
);

create table membership_renewals
(
    id            int  not null primary key generated always as identity,
    renewed_date  date not null default current_date,
    membership_id int  not null references memberships (id),
    created_by    int  not null references users (id),
    created_at    timestamp(2)  default current_timestamp(2),
    updated_at    timestamp(2)  default current_timestamp(2)
);

create table member_attendances
(
    id           int  not null primary key generated always as identity,
    checked_date date not null default current_date,
    member_id    int  not null references members (id),
    created_by   int  not null references users (id),
    created_at   timestamp(2)  default current_timestamp(2),
    updated_at   timestamp(2)  default current_timestamp(2)
);

create table session_types
(
    id         int          not null primary key generated always as identity,
    name       varchar(100) not null,
    price      numeric      not null,
    created_at timestamp(2) default current_timestamp(2),
    updated_at timestamp(2) default current_timestamp(2)
);

create table sessions
(
    id              int          not null primary key generated always as identity,
    full_name       varchar(255) not null,
    session_type_id int          not null references session_types (id),
    created_by      int          not null references users (id),
    created_at      timestamp(2) default current_timestamp(2),
    updated_at      timestamp(2) default current_timestamp(2)
);

create table products
(
    id                  int          not null primary key generated always as identity,
    product_name        varchar(255) not null,
    product_image       text         not null,
    product_description text         not null,
    quantity            int          not null,
    price               numeric      not null,
    added_by            int          not null references users (id),
    created_at          timestamp(2) default current_timestamp(2),
    updated_at          timestamp(2) default current_timestamp(2)
);

create table sales
(
    id          int     not null primary key generated always as identity,
    product_id  int     not null references products (id),
    quantity    int     not null,
    total_price numeric not null check ( total_price > 0 ),
    made_by     int     null default null references users (id),
    created_at  timestamp(2) default current_timestamp(2),
    updated_at  timestamp(2) default current_timestamp(2)
);

-- -- persons
-- -- users
-- -- owners
-- -- employees
-- -- members
-- -- memberships
-- -- membership_types
-- -- membership_renewals
-- -- attendances
-- -- sessions
-- -- session_types
-- -- gym_cards
-- -- products
-- -- sale_items
-- -- sales