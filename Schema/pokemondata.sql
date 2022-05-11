INSERT INTO poke_type ( poke_type_name) 
VALUES ('Normal');
INSERT INTO poke_type ( poke_type_name) 
VALUES ('Fire');
INSERT INTO poke_type ( poke_type_name) 
VALUES ('Grass');
INSERT INTO poke_type ( poke_type_name) 
VALUES ('Electric');
INSERT INTO poke_type ( poke_type_name) 
VALUES ('Ice');
INSERT INTO poke_type ( poke_type_name) 
VALUES ('Fighting');
INSERT INTO poke_type ( poke_type_name) 
VALUES ('Poison');
INSERT INTO poke_type ( poke_type_name) 
VALUES ('Ground');
INSERT INTO poke_type ( poke_type_name) 
VALUES ('Flying');
INSERT INTO poke_type ( poke_type_name) 
VALUES ('Physhic');
INSERT INTO poke_type ( poke_type_name) 
VALUES ('Bug');
INSERT INTO poke_type ( poke_type_name) 
VALUES ('Rock');
INSERT INTO poke_type ( poke_type_name) 
VALUES ('Ghost');
INSERT INTO poke_type ( poke_type_name) 
VALUES ('Dark');
INSERT INTO poke_type ( poke_type_name) 
VALUES ('Dragon');
INSERT INTO poke_type ( poke_type_name) 
VALUES ('Steel');
INSERT INTO poke_type ( poke_type_name) 
VALUES ('Fairy');
INSERT INTO poke_type ( poke_type_name) 
VALUES ('Water');

INSERT INTO poke_move (move_name) 
VALUES ('Flamethrower');
INSERT INTO poke_move (move_name) 
VALUES ('Flame Charge');
INSERT INTO poke_move (move_name) 
VALUES ('Bullet Seed');
INSERT INTO poke_move (move_name) 
VALUES ('Solar Beam');
INSERT INTO poke_move (move_name) 
VALUES ('Tackle');
INSERT INTO poke_move (move_name) 
VALUES ('Ember');

INSERT INTO poke_trainer (trainer_name) 
VALUES ('Red');
INSERT INTO poke_trainer (trainer_name) 
VALUES ('Blue');
INSERT INTO poke_trainer (trainer_name) 
VALUES ('Green');
INSERT INTO poke_trainer (trainer_name) 
VALUES ('Yellow');

INSERT INTO pokemon (poke_name, height_meters, weight_kg, poke_type1_id, pokedex_entry) 
VALUES ('Charmander', 0.6, 8.5, 4, 4);
INSERT INTO pokemon (poke_name, height_meters, weight_kg, poke_type1_id, pokedex_entry) 
VALUES ('Charmeleon', 1.1, 19, 4, 5);
INSERT INTO pokemon (poke_name, height_meters, weight_kg, poke_type1_id, poke_type2_id, pokedex_entry) 
VALUES ('Charizard', 1.7, 90.5, 4, 11, 6);

INSERT INTO my_pokemon (pokedex_entry, nickname, hp, poke_lvl) 
VALUES ('4', 'Mikaela', 12, 5);
INSERT INTO my_pokemon (pokedex_entry, nickname, hp, poke_lvl) 
VALUES ('5', 'Thomas', 32, 16);
INSERT INTO my_pokemon (pokedex_entry, nickname, hp, poke_lvl) 
VALUES ('6', 'Steven', 108, 37);
INSERT INTO my_pokemon (pokedex_entry, nickname, hp, poke_lvl) 
VALUES ('4', 'Greg', 12, 5);
INSERT INTO my_pokemon (pokedex_entry, nickname, hp, poke_lvl) 
VALUES ('5', 'Eduard', 32, 16);
INSERT INTO my_pokemon (pokedex_entry, nickname, hp, poke_lvl) 
VALUES ('6', 'Alan', 108, 37);

INSERT INTO poke_party (trainer_id, my_poke_id) 
VALUES (1, 1);
INSERT INTO poke_party (trainer_id, my_poke_id) 
VALUES (1, 2);
INSERT INTO poke_party (trainer_id, my_poke_id) 
VALUES (2, 3);
INSERT INTO poke_party (trainer_id, my_poke_id) 
VALUES (2, 4);
INSERT INTO poke_party (trainer_id, my_poke_id) 
VALUES (2, 5);
INSERT INTO poke_party (trainer_id, my_poke_id) 
VALUES (3, 6);

INSERT INTO my_poke_moves (my_poke_id, move_id) 
VALUES (3, 1);
INSERT INTO my_poke_moves (my_poke_id, move_id) 
VALUES (3, 2);
INSERT INTO my_poke_moves (my_poke_id, move_id) 
VALUES (3, 3);
INSERT INTO my_poke_moves (my_poke_id, move_id) 
VALUES (3, 6);
INSERT INTO my_poke_moves (my_poke_id, move_id) 
VALUES (1, 5);
INSERT INTO my_poke_moves (my_poke_id, move_id) 
VALUES (2, 6);
INSERT INTO my_poke_moves (my_poke_id, move_id) 
VALUES (2, 2);
INSERT INTO my_poke_moves (my_poke_id, move_id) 
VALUES (2, 5);