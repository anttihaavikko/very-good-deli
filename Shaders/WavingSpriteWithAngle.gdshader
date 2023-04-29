shader_type canvas_item;

uniform float amount = 1.0;
uniform float frequency = 0.01;
uniform float speed = 8.0;
uniform float extra = 0.0;
uniform bool flip = false;

vec2 rotate(vec2 p, float a, vec2 o)
{
  float s = sin(a);
  float c = cos(a);

  p -= o;

  float xnew = p.x * c - p.y * s;
  float ynew = p.x * s + p.y * c;

  return vec2(xnew, ynew) + o;
}

void vertex() {
	vec3 world = (MODEL_MATRIX * vec4(VERTEX, 1.0, 1.0)).xyz;
	float phase = (frequency * world.x) + (speed * TIME);
	vec2 rot = rotate(VERTEX, amount * 0.4 * sin(phase), vec2(0.5, 0.0));
	VERTEX.xy = mix(rot, VERTEX, abs(float(flip) - UV.y * UV.y));
	VERTEX.x += (1.0 * float(!flip) - UV.y * UV.y) * extra * sin(phase);
}
