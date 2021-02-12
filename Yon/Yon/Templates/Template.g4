grammar Template;
CHAR: ~('{' | '}');
PROPERTY_NAME_BEGIN: '{';
PROPERTY_NAME_END: '}';
property_name: PROPERTY_NAME_BEGIN CHAR+ PROPERTY_NAME_END;
template: property_name+;