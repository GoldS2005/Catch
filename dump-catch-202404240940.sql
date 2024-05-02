PGDMP         (    	            |            catch    10.22    15.3 I    P           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            Q           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            R           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            S           1262    32768    catch    DATABASE     g   CREATE DATABASE catch WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'C';
    DROP DATABASE catch;
                postgres    false                        2615    2200    public    SCHEMA        CREATE SCHEMA public;
    DROP SCHEMA public;
                postgres    false            T           0    0    SCHEMA public    COMMENT     6   COMMENT ON SCHEMA public IS 'standard public schema';
                   postgres    false    6            U           0    0    SCHEMA public    ACL     Q   REVOKE USAGE ON SCHEMA public FROM PUBLIC;
GRANT ALL ON SCHEMA public TO PUBLIC;
                   postgres    false    6            �            1259    32845    admins    TABLE     �   CREATE TABLE public.admins (
    id integer NOT NULL,
    first_name character varying(255) NOT NULL,
    last_name character varying(255) NOT NULL,
    email character varying(255) NOT NULL,
    password character varying(255) NOT NULL
);
    DROP TABLE public.admins;
       public            postgres    false    6            �            1259    32843    admins_id_seq    SEQUENCE     �   CREATE SEQUENCE public.admins_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 $   DROP SEQUENCE public.admins_id_seq;
       public          postgres    false    6    205            V           0    0    admins_id_seq    SEQUENCE OWNED BY     ?   ALTER SEQUENCE public.admins_id_seq OWNED BY public.admins.id;
          public          postgres    false    204            �            1259    32782 
   categories    TABLE     f   CREATE TABLE public.categories (
    id integer NOT NULL,
    name character varying(255) NOT NULL
);
    DROP TABLE public.categories;
       public            postgres    false    6            �            1259    32780    categories_id_seq    SEQUENCE     �   CREATE SEQUENCE public.categories_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 (   DROP SEQUENCE public.categories_id_seq;
       public          postgres    false    197    6            W           0    0    categories_id_seq    SEQUENCE OWNED BY     G   ALTER SEQUENCE public.categories_id_seq OWNED BY public.categories.id;
          public          postgres    false    196            �            1259    32817 	   customers    TABLE       CREATE TABLE public.customers (
    id integer NOT NULL,
    first_name character varying(255) NOT NULL,
    last_name character varying(255) NOT NULL,
    email character varying(255) NOT NULL,
    password character varying(255) NOT NULL,
    phone_number character varying(255)
);
    DROP TABLE public.customers;
       public            postgres    false    6            �            1259    32815    customers_id_seq    SEQUENCE     �   CREATE SEQUENCE public.customers_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 '   DROP SEQUENCE public.customers_id_seq;
       public          postgres    false    201    6            X           0    0    customers_id_seq    SEQUENCE OWNED BY     E   ALTER SEQUENCE public.customers_id_seq OWNED BY public.customers.id;
          public          postgres    false    200            �            1259    32832    managers    TABLE     �   CREATE TABLE public.managers (
    id integer NOT NULL,
    first_name character varying(255) NOT NULL,
    last_name character varying(255) NOT NULL,
    email character varying(255) NOT NULL,
    password character varying(255) NOT NULL
);
    DROP TABLE public.managers;
       public            postgres    false    6            �            1259    32830    managers_id_seq    SEQUENCE     �   CREATE SEQUENCE public.managers_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 &   DROP SEQUENCE public.managers_id_seq;
       public          postgres    false    203    6            Y           0    0    managers_id_seq    SEQUENCE OWNED BY     C   ALTER SEQUENCE public.managers_id_seq OWNED BY public.managers.id;
          public          postgres    false    202            �            1259    32917    order_items    TABLE     �   CREATE TABLE public.order_items (
    id integer NOT NULL,
    order_id integer,
    product_id integer,
    quantity integer NOT NULL,
    price numeric(10,2) NOT NULL
);
    DROP TABLE public.order_items;
       public            postgres    false    6            �            1259    32915    order_items_id_seq    SEQUENCE     �   CREATE SEQUENCE public.order_items_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 )   DROP SEQUENCE public.order_items_id_seq;
       public          postgres    false    6    211            Z           0    0    order_items_id_seq    SEQUENCE OWNED BY     I   ALTER SEQUENCE public.order_items_id_seq OWNED BY public.order_items.id;
          public          postgres    false    210            �            1259    32858    orders    TABLE     8  CREATE TABLE public.orders (
    id integer NOT NULL,
    customer_id integer,
    manager_id integer,
    pickup_point_id integer,
    status character varying(255) NOT NULL,
    created_at timestamp without time zone DEFAULT now() NOT NULL,
    updated_at timestamp without time zone DEFAULT now() NOT NULL
);
    DROP TABLE public.orders;
       public            postgres    false    6            �            1259    32856    orders_id_seq    SEQUENCE     �   CREATE SEQUENCE public.orders_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 $   DROP SEQUENCE public.orders_id_seq;
       public          postgres    false    207    6            [           0    0    orders_id_seq    SEQUENCE OWNED BY     ?   ALTER SEQUENCE public.orders_id_seq OWNED BY public.orders.id;
          public          postgres    false    206            �            1259    32806    pickup_points    TABLE     �   CREATE TABLE public.pickup_points (
    id integer NOT NULL,
    name character varying(255) NOT NULL,
    address character varying(255) NOT NULL,
    phone_number character varying(255)
);
 !   DROP TABLE public.pickup_points;
       public            postgres    false    6            �            1259    32804    pickup_points_id_seq    SEQUENCE     �   CREATE SEQUENCE public.pickup_points_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 +   DROP SEQUENCE public.pickup_points_id_seq;
       public          postgres    false    6    199            \           0    0    pickup_points_id_seq    SEQUENCE OWNED BY     M   ALTER SEQUENCE public.pickup_points_id_seq OWNED BY public.pickup_points.id;
          public          postgres    false    198            �            1259    32901    products    TABLE     �   CREATE TABLE public.products (
    id integer NOT NULL,
    name character varying(255) NOT NULL,
    description text,
    price numeric(10,2) NOT NULL,
    category_id integer,
    image bytea
);
    DROP TABLE public.products;
       public            postgres    false    6            �            1259    32899    products_id_seq    SEQUENCE     �   CREATE SEQUENCE public.products_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 &   DROP SEQUENCE public.products_id_seq;
       public          postgres    false    209    6            ]           0    0    products_id_seq    SEQUENCE OWNED BY     C   ALTER SEQUENCE public.products_id_seq OWNED BY public.products.id;
          public          postgres    false    208            �
           2604    32848 	   admins id    DEFAULT     f   ALTER TABLE ONLY public.admins ALTER COLUMN id SET DEFAULT nextval('public.admins_id_seq'::regclass);
 8   ALTER TABLE public.admins ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    205    204    205            �
           2604    32785    categories id    DEFAULT     n   ALTER TABLE ONLY public.categories ALTER COLUMN id SET DEFAULT nextval('public.categories_id_seq'::regclass);
 <   ALTER TABLE public.categories ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    196    197    197            �
           2604    32820    customers id    DEFAULT     l   ALTER TABLE ONLY public.customers ALTER COLUMN id SET DEFAULT nextval('public.customers_id_seq'::regclass);
 ;   ALTER TABLE public.customers ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    201    200    201            �
           2604    32835    managers id    DEFAULT     j   ALTER TABLE ONLY public.managers ALTER COLUMN id SET DEFAULT nextval('public.managers_id_seq'::regclass);
 :   ALTER TABLE public.managers ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    203    202    203            �
           2604    32920    order_items id    DEFAULT     p   ALTER TABLE ONLY public.order_items ALTER COLUMN id SET DEFAULT nextval('public.order_items_id_seq'::regclass);
 =   ALTER TABLE public.order_items ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    211    210    211            �
           2604    32861 	   orders id    DEFAULT     f   ALTER TABLE ONLY public.orders ALTER COLUMN id SET DEFAULT nextval('public.orders_id_seq'::regclass);
 8   ALTER TABLE public.orders ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    207    206    207            �
           2604    32809    pickup_points id    DEFAULT     t   ALTER TABLE ONLY public.pickup_points ALTER COLUMN id SET DEFAULT nextval('public.pickup_points_id_seq'::regclass);
 ?   ALTER TABLE public.pickup_points ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    199    198    199            �
           2604    32904    products id    DEFAULT     j   ALTER TABLE ONLY public.products ALTER COLUMN id SET DEFAULT nextval('public.products_id_seq'::regclass);
 :   ALTER TABLE public.products ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    208    209    209            G          0    32845    admins 
   TABLE DATA           L   COPY public.admins (id, first_name, last_name, email, password) FROM stdin;
    public          postgres    false    205   9R       ?          0    32782 
   categories 
   TABLE DATA           .   COPY public.categories (id, name) FROM stdin;
    public          postgres    false    197   �R       C          0    32817 	   customers 
   TABLE DATA           ]   COPY public.customers (id, first_name, last_name, email, password, phone_number) FROM stdin;
    public          postgres    false    201   �R       E          0    32832    managers 
   TABLE DATA           N   COPY public.managers (id, first_name, last_name, email, password) FROM stdin;
    public          postgres    false    203   �S       M          0    32917    order_items 
   TABLE DATA           P   COPY public.order_items (id, order_id, product_id, quantity, price) FROM stdin;
    public          postgres    false    211   T       I          0    32858    orders 
   TABLE DATA           n   COPY public.orders (id, customer_id, manager_id, pickup_point_id, status, created_at, updated_at) FROM stdin;
    public          postgres    false    207   KT       A          0    32806    pickup_points 
   TABLE DATA           H   COPY public.pickup_points (id, name, address, phone_number) FROM stdin;
    public          postgres    false    199   �T       K          0    32901    products 
   TABLE DATA           T   COPY public.products (id, name, description, price, category_id, image) FROM stdin;
    public          postgres    false    209   kU       ^           0    0    admins_id_seq    SEQUENCE SET     ;   SELECT pg_catalog.setval('public.admins_id_seq', 1, true);
          public          postgres    false    204            _           0    0    categories_id_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public.categories_id_seq', 5, true);
          public          postgres    false    196            `           0    0    customers_id_seq    SEQUENCE SET     >   SELECT pg_catalog.setval('public.customers_id_seq', 2, true);
          public          postgres    false    200            a           0    0    managers_id_seq    SEQUENCE SET     =   SELECT pg_catalog.setval('public.managers_id_seq', 2, true);
          public          postgres    false    202            b           0    0    order_items_id_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public.order_items_id_seq', 5, true);
          public          postgres    false    210            c           0    0    orders_id_seq    SEQUENCE SET     ;   SELECT pg_catalog.setval('public.orders_id_seq', 3, true);
          public          postgres    false    206            d           0    0    pickup_points_id_seq    SEQUENCE SET     B   SELECT pg_catalog.setval('public.pickup_points_id_seq', 2, true);
          public          postgres    false    198            e           0    0    products_id_seq    SEQUENCE SET     =   SELECT pg_catalog.setval('public.products_id_seq', 5, true);
          public          postgres    false    208            �
           2606    32855    admins admins_email_key 
   CONSTRAINT     S   ALTER TABLE ONLY public.admins
    ADD CONSTRAINT admins_email_key UNIQUE (email);
 A   ALTER TABLE ONLY public.admins DROP CONSTRAINT admins_email_key;
       public            postgres    false    205            �
           2606    32853    admins admins_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public.admins
    ADD CONSTRAINT admins_pkey PRIMARY KEY (id);
 <   ALTER TABLE ONLY public.admins DROP CONSTRAINT admins_pkey;
       public            postgres    false    205            �
           2606    32787    categories categories_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public.categories
    ADD CONSTRAINT categories_pkey PRIMARY KEY (id);
 D   ALTER TABLE ONLY public.categories DROP CONSTRAINT categories_pkey;
       public            postgres    false    197            �
           2606    32827    customers customers_email_key 
   CONSTRAINT     Y   ALTER TABLE ONLY public.customers
    ADD CONSTRAINT customers_email_key UNIQUE (email);
 G   ALTER TABLE ONLY public.customers DROP CONSTRAINT customers_email_key;
       public            postgres    false    201            �
           2606    32829 $   customers customers_phone_number_key 
   CONSTRAINT     g   ALTER TABLE ONLY public.customers
    ADD CONSTRAINT customers_phone_number_key UNIQUE (phone_number);
 N   ALTER TABLE ONLY public.customers DROP CONSTRAINT customers_phone_number_key;
       public            postgres    false    201            �
           2606    32825    customers customers_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public.customers
    ADD CONSTRAINT customers_pkey PRIMARY KEY (id);
 B   ALTER TABLE ONLY public.customers DROP CONSTRAINT customers_pkey;
       public            postgres    false    201            �
           2606    32842    managers managers_email_key 
   CONSTRAINT     W   ALTER TABLE ONLY public.managers
    ADD CONSTRAINT managers_email_key UNIQUE (email);
 E   ALTER TABLE ONLY public.managers DROP CONSTRAINT managers_email_key;
       public            postgres    false    203            �
           2606    32840    managers managers_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public.managers
    ADD CONSTRAINT managers_pkey PRIMARY KEY (id);
 @   ALTER TABLE ONLY public.managers DROP CONSTRAINT managers_pkey;
       public            postgres    false    203            �
           2606    32922    order_items order_items_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.order_items
    ADD CONSTRAINT order_items_pkey PRIMARY KEY (id);
 F   ALTER TABLE ONLY public.order_items DROP CONSTRAINT order_items_pkey;
       public            postgres    false    211            �
           2606    32865    orders orders_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_pkey PRIMARY KEY (id);
 <   ALTER TABLE ONLY public.orders DROP CONSTRAINT orders_pkey;
       public            postgres    false    207            �
           2606    32814     pickup_points pickup_points_pkey 
   CONSTRAINT     ^   ALTER TABLE ONLY public.pickup_points
    ADD CONSTRAINT pickup_points_pkey PRIMARY KEY (id);
 J   ALTER TABLE ONLY public.pickup_points DROP CONSTRAINT pickup_points_pkey;
       public            postgres    false    199            �
           2606    32909    products products_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public.products
    ADD CONSTRAINT products_pkey PRIMARY KEY (id);
 @   ALTER TABLE ONLY public.products DROP CONSTRAINT products_pkey;
       public            postgres    false    209            �
           2606    32923 %   order_items order_items_order_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.order_items
    ADD CONSTRAINT order_items_order_id_fkey FOREIGN KEY (order_id) REFERENCES public.orders(id);
 O   ALTER TABLE ONLY public.order_items DROP CONSTRAINT order_items_order_id_fkey;
       public          postgres    false    2746    211    207            �
           2606    32928 '   order_items order_items_product_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.order_items
    ADD CONSTRAINT order_items_product_id_fkey FOREIGN KEY (product_id) REFERENCES public.products(id);
 Q   ALTER TABLE ONLY public.order_items DROP CONSTRAINT order_items_product_id_fkey;
       public          postgres    false    2748    211    209            �
           2606    32866    orders orders_customer_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_customer_id_fkey FOREIGN KEY (customer_id) REFERENCES public.customers(id);
 H   ALTER TABLE ONLY public.orders DROP CONSTRAINT orders_customer_id_fkey;
       public          postgres    false    201    207    2736            �
           2606    32871    orders orders_manager_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_manager_id_fkey FOREIGN KEY (manager_id) REFERENCES public.managers(id);
 G   ALTER TABLE ONLY public.orders DROP CONSTRAINT orders_manager_id_fkey;
       public          postgres    false    207    2740    203            �
           2606    32876 "   orders orders_pickup_point_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_pickup_point_id_fkey FOREIGN KEY (pickup_point_id) REFERENCES public.pickup_points(id);
 L   ALTER TABLE ONLY public.orders DROP CONSTRAINT orders_pickup_point_id_fkey;
       public          postgres    false    2730    207    199            �
           2606    32910 "   products products_category_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.products
    ADD CONSTRAINT products_category_id_fkey FOREIGN KEY (category_id) REFERENCES public.categories(id);
 L   ALTER TABLE ONLY public.products DROP CONSTRAINT products_category_id_fkey;
       public          postgres    false    2728    209    197            G   J   x�3�0�{.츰����.6\�p��¾�x�.컰�31%73�!�"1� 'U/9?"bhd����� �82�      ?   [   x����@c�$�n(�DB\ ҉��qGXh�ݙͅ���M��������.����i�_*a���C���Z�������l�&S���7�      C   u   x�3�0�¦.�3.컰�3�,1/��!�"1� 'U/9?������ԌS�\A���XS��51�53�2�0��֋M��!�%Eh����B1�T073�51�52����� �'63      E   p   x�3�0��v\�缰�� �����.l����87�(/�,�!�"1� 'U/9?�371/1=���Ș��e+P��[/��0�o��� ݜ��U9�e�t���q��qqq j)<�      M   8   x�3�4B#NC �30�2�p9͠|c �-!\ ���7J��̀�c���� g      I   y   x�3�4�s/컰�b����FFƺF���
�FV@�M�ˈ/̻��bÅ6^�p�	h�& {+��x���D���0��1��.6]�6nӅ��^؋Pn�`h�nB�+F��� I?B      A   �   x���1
�@E�SL��.�&�Żxcaa�,D�@	�=ß9�6�X���?q8�7�Z3Z=�C�{l�aSS`����������.���"�l-2g��/��2ſ�/��E��n1�?~*?��J�V�V��/�6����l�      K   |  x��S�n�P]�|�|@�l'��
u�	�.K��%'FQ@]6	� ��D��a⼜_��G�;EB�(��5s�93�3|�s�y͹���wXn�q�O���$|���~�������=B	�k�U��z�xÙ�N����Ƃ7<�=♜���!��y��$ �Z�ܾ��CMn�	W.&+;⭽����l%�9��|��+N��븮�L�u^���F���O8����^�K�/�m�Ǟ��:a7�q�}��K>��
 ���K..�/Rl�j�Ky��#�@�T��;l���+5�(�ױ��GU (�����x阣J_�i�&�)�m�Ia�J+؏{Na�I�qv����'����'�[�	V�������)��YR �f���2� �Fy�ـp���4%}a��k��4J*C���� ���UZ��Z�F��Wa��^tuØN�$�{�u蘇b���SUV��C�|�l�Q2�SGdZuqiF
Cʠ@�o���6��i��o�Ο*M�H�"j4U���g��@�+�JA���@���|)-��'�y�5U��vOM�{kh���������aw�T{َ�3hs�Sn�a�ݔ��~!�l�T�]��b?v�Wy"P�-�V����5,     