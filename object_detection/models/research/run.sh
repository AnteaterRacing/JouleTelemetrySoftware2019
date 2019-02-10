python3 object_detection/dataset_tools/create_cone_tf_record.py --label_map_path=object_detection/data/cone_label_map.pbtxt --data_dir=`pwd` --output_dir=`pwd`

python3 object_detection/legacy/train.py  \
    --pipeline_config_path=${PIPELINE_CONFIG_PATH} \
    --train_dir=${MODEL_DIR} \
    --num_train_steps=${NUM_TRAIN_STEPS} \
    --sample_1_of_n_eval_examples=$SAMPLE_1_OF_N_EVAL_EXAMPLES \
    --alsologtostder

python3 object_detection/export_inference_graph.py \
	--inut_type image_tensor \
	--pipeline_config_path FULL_CONE_BUCKET_SSD_MODEL/models/model/ssd_mobilenet_v1_coco.config \
	--trained_checkpoint_prefix FULL_CONE_BUCKET_SSD_MODEL/models/model/train/model.ckpt-2000 \
	--output_directory FULL_CONE_BUCKET_SSD_MODEL/models/model/export
