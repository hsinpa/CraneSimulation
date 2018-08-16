using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General {
	public const int interactableLayer = 9;
	public const int interactableLayerMask = 1 << 9;

}

public class DeductionStandard {
	public class StartPoint {
		public const string string_not_tide = "startpoint@string_not_tide";
		public const string bargage_leave_instantly = "startpoint@bargage_leave_instantly";
		public const string touch_examine_line = "startpoint@touch_examine_line";
		public const string swing_slightly = "startpoint@swing-40-80cm";
		public const string swing_overly = "startpoint@swing-over-80cm";
		public const string red_range_before_moving = "startpoint@red_range_before_moving";
		public const string black_range_before_moving = "startpoint@black_range_before_moving";		
	}

	public class Constraint {
		public const string slight_impact = "constraint@slight_impact";
		public const string knock_down = "constraint@knock_down";
		public const string yellow_range = "constraint@yellow_range";
		public const string red_range = "constraint@red_range";
		public const string white_range = "constraint@white_range";

		public const string black_range = "constraint@black_range";
		public const string over_object_height = "constraint@over_object_height";
		public const string below_object_height = "constraint@below_object_height";
	}

	public class Movement {
		public const string over_swing = "movement@over_swing";
		public const string incorrect_control_behavior = "movement@incorrect_control_behavior";
		public const string exceed_path = "movement@exceed_path";
		public const string exceed_height_50cm = "movement@exceed_height_50cm";
		public const string bargage_touch_ground = "movement@bargage_touch_ground";
		public const string negative_control = "movement@negative_control";
		public const string stop_instantly = "movement@stop_instantly";
	}

	public class Ending {
		public const string no_stop_before_landing = "ending@no_stop_before_landing";
		public const string bargage_touch_ground = "ending@bargage_touch_ground";
		public const string touch_examine_line = "ending@touch_examine_line";
		public const string wrong_landing_position = "ending@wrong_landing_position";
	}

	public class ExamFail {
		public const string over_time = "exam_fail@over_time";
		public const string exceed_path_meter = "exam_fail@exceed_path_meter";
		public const string over_swing = "exam_fail@over_swing";
		public const string move_pass_buildings = "exam_fail@move_pass_buildings";
	}

}
