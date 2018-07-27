select * from cards

select * from card_templates;

SELECT * FROM cards JOIN card_templates on cards.template_id = card_templates.id

INSERT INTO cards (template_id, to_email, to_name, message, from_name, from_email)
VALUES(3, 'test@test.com', 'toNameField', 'The Message goes Here', 'fromNameField', 'From@email.com');

INSERT INTO cards (template_id, to_email, to_name, message, from_name, from_email)" +
                                         "VALUES(@templateID, @toEmail, @toName, @message, @froName, @froEmail);


SELECT * FROM cards JOIN card_templates on cards.template_id = card_templates.id
WHERE cards.template_id = '2' AND cards.to_email = 'denise@gmail.com' AND cards.to_name = 'Denise'
AND cards.message = 'GetWell' AND cards.from_name = 'Kirk' AND cards.from_email = 'kirk@gmail.com'
AND card_templates.id = '2';

SELECT cards.template_id, cards.to_email, cards.to_name, cards.message, cards.from_name, cards.from_email, card_templates.name, card_templates.imageName, card_templates.fontColor
FROM cards JOIN card_templates on cards.template_id = card_templates.id
WHERE cards.template_id = '2' AND cards.to_email = 'denise@gmail.com' AND cards.to_name = 'Denise'
AND cards.message = 'GetWell' AND cards.from_name = 'Kirk' AND cards.from_email = 'kirk@gmail.com'
AND card_templates.id = '2'